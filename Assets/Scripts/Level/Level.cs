using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{
    #region Fields

    [SerializeField] private Letter letterPrefab;
    [SerializeField] private BubbleColumn bubbleColumnPrefab;
    [SerializeField] private RectTransform letterContainer;
    [SerializeField] private RectTransform bubbleContainer;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Hammer hammer;
    [SerializeField] private WordDictionary wordDictionary;
    [SerializeField] private int w;

    List<BubbleColumn> columns;


    public event Action<bool> LevelFinished;


    private LevelData currentLevelData;
    private List<Letter> instantiatedLetters = new List<Letter>();
    private HashSet<string> usedWords;

    #endregion



    #region Methods

    public void Initialize(int index)
    {
        currentLevelData = LevelsStorage.Instance.GetLevelData(index);
        resetButton.onClick.AddListener(ResetCurrent);
        pauseButton.onClick.AddListener(PauseButtonOnClick);

        hammer.Initialize();

        Bubble.OnBubbleClick += OnBubbleClick;
        hammer.OnHammerOnBubbleActivated += OnHammerActivated;
        
        usedWords = new HashSet<string>();

        InitializeLetters();
        InitializeBubbles();
        
        Letter.OnChanged += OnChanged;
    }


    private void OnChanged()
    {
        LayoutRebuilder.MarkLayoutForRebuild(letterContainer);
    }


    public void Deinitialize()
    {
        currentLevelData = null;
        resetButton.onClick.RemoveListener(ResetCurrent);
        pauseButton.onClick.RemoveListener(PauseButtonOnClick);

        hammer.Deinitialize();

        Bubble.OnBubbleClick -= OnBubbleClick;
        hammer.OnHammerOnBubbleActivated -= OnHammerActivated;

        DeinitializeLetters();
        DeinitializeBubbles();
        
        Letter.OnChanged -= OnChanged;
    }


    private void InitializeBubbles()
    {
        columns = new List<BubbleColumn>();
        foreach (var count in currentLevelData.Matrix)
        {
            var column = Instantiate(bubbleColumnPrefab, bubbleContainer);
            column.Initialize(count);
            columns.Add(column);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(bubbleContainer);
    }


    private void DeinitializeBubbles()
    {
        foreach (var column in columns)
        {
            column.Deinitialize();
            Destroy(column.gameObject);
        }

        columns.Clear();
    }


    private void InitializeLetters()
    {
        for (int i = 0; i < currentLevelData.Letters.Count; i++)
        {
            var c = currentLevelData.Letters[i];
            var letter = Instantiate(letterPrefab, letterContainer);
            instantiatedLetters.Add(letter);
            var lp = letterContainer.position;
            lp.Set(lp.x + (w * (i + 0.5f - currentLevelData.Letters.Count / 2.0f) / currentLevelData.Letters.Count),
                lp.y, lp.z);
            letter.Initialize(c, lp);
        }
    }


    private void DeinitializeLetters()
    {
        foreach (var letter in instantiatedLetters)
        {
            letter.Deinitialize();
            Destroy(letter.gameObject);
        }

        instantiatedLetters.Clear();
    }


    private void ResetCurrent()
    {
        foreach (var bubble in columns.SelectMany(c => c.Bubbles))
        {
            bubble.ClearLetter();
        }

        foreach (var letter in instantiatedLetters)
        {
            letter.gameObject.SetActive(true);
        }
    }


    private void FinishLevel(bool result)
    {
        LevelFinished?.Invoke(result);
    }


    private void OnBubbleClick(Bubble obj)
    {
        bool isComplete = false;
        
        var word = new List<Bubble>();

        foreach (var column in columns)
        {
            if (column.Bubbles.Last() == obj)
            {
                if (column.Bubbles.All(i => i.Letter != default))
                {
                    if (ValidateWord(GetWord(column.Bubbles)))
                    {
                        word.AddRange(column.Bubbles);
                        isComplete = true;
                    }
                }

                break;
            }
        }

        if (!isComplete)
        {
            int reversIndex = - 1;
            
            foreach (var column in columns)
            {
                for (var i = 0; i < column.Bubbles.Count; i++)
                {
                    if (column.Bubbles[i] == obj)
                    {
                        reversIndex = column.Bubbles.Count - i - 1;
                    }
                }
            }

            var wasEmpty = false;
            
            foreach (var column in columns)
            {
                if (column.Bubbles.Count - reversIndex > 0)
                {
                    if (wasEmpty)
                    {
                        word.Clear();
                        wasEmpty = false;
                    }

                    word.Add(column.Bubbles[column.Bubbles.Count - reversIndex - 1]);
                }
                else
                {
                    wasEmpty = true;
                }
            }

            if (word.Last() == obj)
            {
                if (ValidateWord(GetWord(word)))
                {
                    isComplete = true;
                }
            }
        }

        if (isComplete)
        {
            DestroyBubbles(word);
            ResetCurrent();
        }
    }


    private void DestroyBubbles(List<Bubble> word)
    {
        var columnsToDelete = new List<BubbleColumn>();
        foreach (var column in columns)
        {
            column.DestroyBubbles(word);
            if (column.Bubbles.Count == 0)
            {
                columnsToDelete.Add(column);
            }
        }
        
        foreach (var column in columnsToDelete)
        {
            columns.Remove(column);
            column.Deinitialize();
            DestroyImmediate(column.gameObject);
        }
        
        if (columns.Count == 0)
        {
            FinishLevel(true);
        }

        foreach (var column in columns)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)column.transform);
        }
    }


    private void OnHammerActivated(Bubble bubble)
    {
        DestroyBubbles(new List<Bubble>{bubble});
    }


    private static string GetWord(IEnumerable<Bubble> bubbles)
    {
        var word = string.Empty;
        foreach (var bubble in bubbles)
        {
            word += bubble.Letter;
        }

        return word;
    }


    private bool ValidateWord(string word)
    {
        if (word.Length > 2)
        {
            if (wordDictionary.Validate(word))
            {
                if (!usedWords.Contains(word))
                {
                    usedWords.Add(word);
                    return true;
                }
            }
        }

        return false;
    }


    private void PauseButtonOnClick()
    {
        UiManager.Instance.Show<UiPopupBase>(UiPopupType.Pause);
    }

    #endregion
}