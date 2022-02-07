using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsStorage", menuName = "ScriptableObjects/LevelsStorage")]
public class LevelsStorage : ScriptableSingleton<LevelsStorage>
{
    #region Fields

    [SerializeField] private List<LevelData> levels;

    #endregion



    #region Methods

    public LevelData GetLevelData(int index)
    {
        return levels[index % levels.Count];
    }

    #endregion
}