using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UiPopupLevelWin : UiPopupBase
{
    #region Fields

    [SerializeField] private Button close;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text complimentsView;
    [SerializeField] private List<string> compliments;
    [SerializeField] private string pattern;

    #endregion



    #region Methods

    public override void Initialize()
    {
        base.Initialize();
        close.onClick.AddListener(Close);
        complimentsView.text = compliments[Random.Range(0, compliments.Count)];
    }


    public override void Deinitialize()
    {
        base.Deinitialize();
        close.onClick.AddListener(Close);
    }


    public void SetLevel(int level)
    {
        text.text = string.Format(pattern, level);
    }

    #endregion
}