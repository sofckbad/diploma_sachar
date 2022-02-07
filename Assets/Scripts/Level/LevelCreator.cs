using UnityEngine;


public class LevelCreator : MonoBehaviour
{
    #region Fields

    [SerializeField] private Level level;

    #endregion



    #region Properties

    #endregion



    #region Unity Lifecycle

    private void Start()
    {
        InitializeGame();
    }


    private void OnDestroy()
    {
        DeinitializeGame();
    }

    #endregion



    #region Methods

    public void InitializeGame()
    {
        level.LevelFinished += LevelOnLevelFinished;
        
        CreateLevel();
    }


    private void LevelOnLevelFinished(bool isWin)
    {
        if (isWin)
        {
            DataStorage.Instance.CurrentLevelNumber++;
        }
        
        DestroyLevel();
        var screen = UiManager.Instance.Show<UiPopupLevelWin>(UiPopupType.LevelWin);
        screen.SetLevel(DataStorage.Instance.CurrentLevelNumber);
        screen.AddCloseCallback(CreateLevel);
    }


    public void DeinitializeGame()
    {
        DestroyLevel();
    }
    
    private void CreateLevel()
    {
        level.Initialize(DataStorage.Instance.CurrentLevelNumber);
    }
    
    private void DestroyLevel()
    {
        level.Deinitialize();

    }

    #endregion
}