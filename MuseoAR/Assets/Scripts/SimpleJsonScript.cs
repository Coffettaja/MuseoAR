using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJsonScript : MonoBehaviour {

    private List<Texts> textList;
    private string fileName;

    /// <summary>
    /// Script that will get the i:th text from a simple JSON-file.
    /// </summary>
    /// <param name="i">Number of the entry</param>
    /// <param name="file">Name of the JSON-file</param>
    /// <returns>Text as a string</returns>
    public string getEntry(int i, string file)
    {
        fileName = file;
        textList = new List<Texts>();
        fromJsonToList();

        // If i == -1, a random rumber (between given boundaries) will be generated.
        if (i == -1)
        {
            System.Random rnd = new System.Random();
            i = rnd.Next(0, textList.Count);
        }

        Texts valittu = textList[i];
        string text = valittu.text;
        return text;
    }

    /// <summary>
    /// Transforming a simple JSON-file into a list of objects.
    /// </summary>
    private void fromJsonToList()
    {
        TextAsset ladattava = Resources.Load<TextAsset>(fileName);
        rootText root = JsonUtility.FromJson<rootText>(ladattava.text);

        foreach (var q in root.texts)
        {
            textList.Add(q);
        }
    }


    [Serializable]
    public class Texts
    {
        public string text;

        public override string ToString()
        {
            return string.Format("Info: {0}",
                                 text);
        }
    }

    [Serializable]
    public class rootText
    {
        public Texts[] texts;
    }

    #region Singleton creation
    private static object _lock = new object();
    private static SimpleJsonScript _instance;
    public static SimpleJsonScript Instance
    {
        get
        {
            // Locks down the thread until the the Singleton instance has been created.
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (SimpleJsonScript)FindObjectOfType(
                        typeof(SimpleJsonScript));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<SimpleJsonScript>();
                        singletonObject.name = typeof(SimpleJsonScript).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }
            return _instance;
        }
    }
    #endregion
}
