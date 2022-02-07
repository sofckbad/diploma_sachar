public class DataStorage : Singleton<DataStorage>
{
    #region Fields

    

    #endregion



    #region Properties

    public int CurrentLevelNumber
    {
        get => CustomPlayerPrefs.GetInt(PrefsKeys.CurrentLevelNumber);
        set => CustomPlayerPrefs.SetInt(PrefsKeys.CurrentLevelNumber, value, true);
    }

    #endregion
}