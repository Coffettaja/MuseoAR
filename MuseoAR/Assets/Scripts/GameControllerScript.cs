using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    [System.Serializable]
    public struct SceneDictItem
    {
        public string name;

        // Used for example for marking the status on the map and unlocking selfie items.
        public bool completed;

        /// <summary>
        /// Sets the name and completed status for the SceneDictItem.
        /// Currently, if a scene is completed, it just means that the user has
        /// entered the scene at least once and returned back to the main scene.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="completed"></param>
        public SceneDictItem(string name, bool completed)
        {
            this.name = name;
            this.completed = completed;
        }
    }
    
    private bool _shuttingDown = false;
    private static object _lock = new object();


    public static string tldrIdentifier = "not specified tldr";
    public static string aarreIdentifier = "not specified aarre";
    public static List<int> aarteet = new List<int>();

  #region Singleton creation
  private static GameControllerScript _instance;
    public static GameControllerScript Instance
    {
        get {
            // Locks down the thread until the the Singleton instance has been created.
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
  #endregion

  private void OnDestroy()
    {
        _shuttingDown = true;
    }

    // To keep track of the completed status of a scene, it has to be added here first.
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

    // LoadSceneWithName, but has information about which marker was used.
    public void LoadSceneWithName(string name, string paramTldr, string paramAarre)
    {
        tldrIdentifier = paramTldr;
        aarreIdentifier = paramAarre;
        _currentScene = name;
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Marks the current scene completed and then loads the main scene (named "init").
    /// </summary>
    public void LoadTopLevelScene()
    {
        MarkSceneCompleted(_currentScene);
        LoadSceneWithName("init");
    }

    /// <summary>
    /// Checks if list already has the ID. If not, adds it and adds some points to the score.
    /// </summary>
    /// <param name="aarre">ID of the found treasure</param>
    public void LisaaAarre(int aarre)
    {
        var exists = aarteet.Contains(aarre);
        if (!exists) { aarteet.Add(aarre); }
        if (!exists) { ScoreScript.Instance.IncreaseScoreBy(10); }
    }


  #region Activating decorations for use in selfie scene
  private List<string> activatedDecorations = new List<string>();

  public void ActivateDecorations(string decorationName)
  {
    activatedDecorations.Add(decorationName);
  }

  public void ActivateDecorations(string[] decorationNameArray)
  {
    activatedDecorations.AddRange(decorationNameArray);
  }

  public List<string> GetActivatedDecorations()
  {
    return activatedDecorations;
  }
  #endregion
}

