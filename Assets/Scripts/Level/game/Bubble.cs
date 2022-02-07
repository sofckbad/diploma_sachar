using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Bubble : MonoBehaviour
{
    #region Fields
    
    [SerializeField] private TMP_Text textView;
    [SerializeField] private Button button;
    
    public static event Action<Bubble> OnBubbleClick;
    private char letter;

    #endregion



    #region Properties


    public char Letter
    {
        get
        {
            return letter;
        }
        private set
        {
            letter = value;
            textView.text = value.ToString();
        }
    }

    #endregion



    #region Methods

    public void Initialize()
    {
        button.onClick.AddListener(OnClick);
    }


    public void Deinitialize()
    {
        button.onClick.RemoveListener(OnClick);
    }
    
    public bool SetLetter(char c)
    {
        if (letter == default)
        {
            Letter = c;
            return true;
        }
        return false;
    }

    #endregion



    public void ClearLetter()
    {
        Letter = default;
    }
    
    


    private void OnClick()
    {
        OnBubbleClick?.Invoke(this);
    }
}