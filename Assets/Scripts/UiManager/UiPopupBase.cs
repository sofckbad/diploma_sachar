using System;
using UnityEngine;


public abstract class UiPopupBase : MonoBehaviour
{
    #region Properties

    [SerializeField] private UiPopupType type;


    private Action onCloseAction;

    #endregion



    #region Properties

    public UiPopupType Type => type;

    #endregion



    #region Methods

    public virtual void Initialize()
    {
    }


    public virtual void Deinitialize()
    {
    }


    public virtual void Show()
    {
    }


    public virtual void Hide()
    {
        onCloseAction?.Invoke();
    }


    public void AddCloseCallback(Action evt)
    {
        onCloseAction += evt;
    }


    protected virtual void Close()
    {
        UiManager.Instance.Hide(this);
    }

    #endregion
}