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
    }

    public static GameControllerScript gameManagerInstance;

    public SceneDictItem[] sceneDict;

    private string _currentScene;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        } else if (gameManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void setSceneDictValue(string name, bool value)
    {
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

