using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableSingleton", menuName = "ScriptableObjects/ScriptableSingleton")]
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{

    #region Fields
    
    private static T instance;

    #endregion



    #region Properties
    
    public static T Instance => instance ? instance : instance = GetInstance();

    #endregion



    #region Methods
    
    static T GetInstance()
    {
        var instances = Resources.LoadAll<T>("Configs");
        
        if (instances.Length == 0)
        {
            Debug.LogError($"No links {typeof(T)}. Create any asset");
            return null;
        }

        return instance = instances[0];
    }

    #endregion

}