using UnityEngine;
using UnityEngine.UI;


public class UiPopupPause : UiPopupBase
{
    #region Fields
    
    [SerializeField] private Button close;
    
    #endregion



    #region Methods

    public override void Initialize()
    {
        base.Initialize();
        close.onClick.AddListener(Close);
    }


    public override void Deinitialize()
    {
        base.Deinitialize();
        close.onClick.RemoveListener(Close);
    }

    #endregion
}