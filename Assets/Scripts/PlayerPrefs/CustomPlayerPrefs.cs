using UnityEngine;


public static class CustomPlayerPrefs
{
    #region Getters

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }


    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }


    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    #endregion



    #region Setters

    public static void SetInt(string key, int value, bool saveImmediately = false)
    {
        PlayerPrefs.SetInt(key, value);

        SavePrefsIfNeed(saveImmediately);
    }


    public static void SetFloat(string key, float value, bool saveImmediately = false)
    {
        PlayerPrefs.SetFloat(key, value);

        SavePrefsIfNeed(saveImmediately);
    }


    public static void SetString(string key, string value, bool saveImmediately = false)
    {
        PlayerPrefs.SetString(key, value);

        SavePrefsIfNeed(saveImmediately);
    }

    #endregion



    #region Common

    private static void SavePrefsIfNeed(bool saveImmediately)
    {
        if (saveImmediately)
        {
            PlayerPrefs.Save();
        }
    }

    #endregion
}