using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WordDictionary", menuName = "ScriptableObjects/WordDictionary")]
public class WordDictionary : ScriptableObject, ISerializationCallbackReceiver
{
    #region Fields

    private HashSet<string> words;
    [SerializeField] private List<string> serializedWords;

    #endregion



    #region Methods

    public bool Validate(string word)
    {
        return words.Contains(word);
    }


    #if UNITY_EDITOR
    public void Reg(IEnumerable<string> newWords)
    {
        words = new HashSet<string>(newWords);
    }
    #endif

    #endregion



    #region ISerializationCallbackReceiver

    public void OnBeforeSerialize()
    {
        serializedWords = new List<string>(words);
    }


    public void OnAfterDeserialize()
    {
        words = new HashSet<string>(serializedWords);
    }

    #endregion
}