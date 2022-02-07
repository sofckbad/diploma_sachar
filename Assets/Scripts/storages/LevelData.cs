using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject, ISerializationCallbackReceiver
{
    #region Fields

    public string word;
    [SerializeField, HideInInspector] private char[] letters;
    [SerializeField] private List<int> matrix;

    #endregion



    #region Properties

    public IReadOnlyList<char> Letters => letters;


    public IEnumerable<int> Matrix => matrix;

    #endregion



    public void OnBeforeSerialize()
    {
        letters = word.ToLower().ToCharArray();
    }


    public void OnAfterDeserialize()
    {
    }
}