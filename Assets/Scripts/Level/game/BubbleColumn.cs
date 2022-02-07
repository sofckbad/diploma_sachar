using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BubbleColumn : MonoBehaviour
{
    #region Fields

    [SerializeField] private Bubble prefab;

    private List<Bubble> bubbles;

    #endregion



    #region Properties

    public IReadOnlyList<Bubble> Bubbles => bubbles;

    #endregion



    #region Methods

    public void Initialize(int count)
    {
        bubbles = new List<Bubble>();
        for (int i = 0; i < count; i++)
        {
            var bubble = Instantiate(prefab, transform);
            bubbles.Add(bubble);
            bubble.Initialize();
        }
    }


    public void Deinitialize()
    {
        foreach (var bubble in bubbles)
        {
            bubble.Deinitialize();
            DestroyImmediate(bubble.gameObject);
        }
        
        bubbles.Clear();
    }

    #endregion



    public void DestroyBubbles(List<Bubble> bubblesWord)
    {
        var toDel = new List<Bubble>();
        foreach (var bubble in bubbles)
        {
            if (bubblesWord.Contains(bubble))
            {
                toDel.Add(bubble);
            }
        }
        
        foreach (var bubble in toDel)
        {
            bubbles.Remove(bubble);
            bubble.Deinitialize();
            DestroyImmediate(bubble.gameObject);
        }
        
        toDel.Clear();
        
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }
}