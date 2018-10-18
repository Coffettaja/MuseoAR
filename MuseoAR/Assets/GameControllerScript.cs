using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    public Dictionary<string, bool> sceneDict;

    private string _currentScene;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MarkSceneCompleted(string name)
    {
        sceneDict[name] = true;
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

