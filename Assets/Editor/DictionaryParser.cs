using UnityEditor;
using UnityEngine;


public class DictionaryParser : EditorWindow
{
    #region Fields

    private static TextAsset textAsset;
    private static WordDictionary dictionary;
    private static string val;

    #endregion



    #region Window Lifecycle

    [MenuItem("Auto/DictionaryParser")]
    public static void Run()
    {
        CreateWindow<DictionaryParser>();
    }


    public void OnGUI()
    {
        dictionary = (WordDictionary)EditorGUILayout.ObjectField("dictionary", dictionary, typeof(WordDictionary), false);
        
        val = EditorGUILayout.TextField(val);

        if (GUILayout.Button("Check word"))
        {
            Debug.Log($"слово => {val} => {dictionary.Validate(val)}");
        }
    }

    #endregion
}