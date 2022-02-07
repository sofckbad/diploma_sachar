using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private List<UiPopupBase> popupContainer;

    private List<UiPopupBase> activePopups = new List<UiPopupBase>();

    private static UiManager instance;

    #endregion



    #region Properties

    public static UiManager Instance => instance;

    #endregion



    #region Methods

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"more than 1 {typeof(UiManager)}");
        }
        instance = this;
        DontDestroyOnLoad(this);
    }


    public T Show<T>(UiPopupType type) where T : UiPopupBase
    {
        UiPopupBase popup = popupContainer.FirstOrDefault(i => i.Type == type);

        if (popup != null)
        {
            popup = Instantiate(popup, transform);
            popup.Initialize();
            popup.Show();

            activePopups.Add(popup);
        }

        return popup as T;
    }


    public void Hide(UiPopupType type)
    {
        Hide(activePopups.FirstOrDefault(i => i.Type == type));
    }


    public void Hide(UiPopupBase popup)
    {
        activePopups.Remove(popup);
        popup.Hide();
        popup.Deinitialize();
        Destroy(popup.gameObject);
    }

    #endregion
}