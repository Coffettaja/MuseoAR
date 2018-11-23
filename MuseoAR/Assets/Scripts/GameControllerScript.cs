using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    [System.Serializable]
    public struct SceneDictItem
    {
        public string name;
        public bool completed;

        public SceneDictItem(string name, bool completed)
        {
            this.name = name;
            this.completed = completed;
        }
    }

    private bool _shuttingDown = false;
    private static object _lock = new object();
    private static GameControllerScript _instance;
    public static GameControllerScript Instance
    {
        get {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (GameControllerScript)FindObjectOfType(
                        typeof(GameControllerScript));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<GameControllerScript>();
                        singletonObject.name = typeof(GameControllerScript).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
    }


    public SceneDictItem[] sceneDict = { new SceneDictItem("invaders", false), new SceneDictItem("360VideScene", false) };

    private string _currentScene;


    void Awake()
    {
        //    DontDestroyOnLoad(gameObject);
        //    if (gameManagerInstance == null)
        //    {
        //        gameManagerInstance = this;
        //    } else if (gameManagerInstance != this)
        //    {
        //        Destroy(gameObject);
        //    }
        _currentScene = SceneManager.GetActiveScene().name;
    }

    private void setSceneDictValue(string name, bool value)
    {
        Debug.Log("Marking scene " + name + " completed");
        for (int i = 0; i < sceneDict.Length; i++)
        {
            if (sceneDict[i].name == name)
            {
                sceneDict[i].completed = value;
                return;
            }
        }
    }

    private bool GetSceneDictValue(string name)
    {
        for (int i = 0; i < sceneDict.Length; i++)
        {
            if (sceneDict[i].name == name)
            {
                return sceneDict[i].completed;
            }
        }

        return false;
    }

    public bool IsSceneCompleted(string name)
    {
        return GetSceneDictValue(name);
    }

    public void MarkSceneCompleted(string name)
    {
        setSceneDictValue(name, true);
    }

    public void LoadSceneWithName(string name)
    {
        _currentScene = name;
        SceneManager.LoadScene(name);
    }

    public void LoadTopLevelScene()
    {
        MarkSceneCompleted(_currentScene);
        LoadSceneWithName("init");
    }
}

