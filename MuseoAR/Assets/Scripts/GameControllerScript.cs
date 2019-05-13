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

    // Stores the ID of tldr-marker that was scanned.
    public static string tldrIdentifier = "";

    // Stores the ID of treasure-marker that was scanned.
    public static string aarreIdentifier = "";

    // Player's name and profession in a string.
    public static string nimiJaAmmatti = "nimi";

    // List of visited tldr-scenes.
    public static List<string> tldrScenes = new List<string>();

    // List of visited treasure-scenes.
    public static List<string> aarreScenes = new List<string>();

    // List of other visited scenes.
    public static List<string> otherScenes = new List<string>();

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

        // Determining which marker was scanned, adding it to corresponding list of scanned markers, and adding points to player
        // if marker was not scanned before.
        if (paramTldr != "")
        {
            var exists = tldrScenes.Contains(paramTldr);
            if (!exists) { tldrScenes.Add(paramTldr); ScoreScript.Instance.IncreaseScoreBy(10); }
        }
        else
        {
            if (paramAarre != "")
            {
                var exists = aarreScenes.Contains(paramAarre);
                if (!exists) { aarreScenes.Add(paramAarre); ScoreScript.Instance.IncreaseScoreBy(20); }
            }
            else
            {
                var exists = otherScenes.Contains(name);
                if (!exists) { otherScenes.Add(name); ScoreScript.Instance.IncreaseScoreBy(10); }
            }
        }

        if (name == "reset")
        {
            ResetAll();
        }
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Marks the current scene completed and then loads the main scene (named "init").
    /// </summary>
    public void LoadTopLevelScene()
    {
    MarkSceneCompleted(_currentScene);
        LoadSceneWithName("init");
        Debug.Log(nimiJaAmmatti);
    }

    /// <summary>
    /// Resets all relevant information and transitions to the splash screen.
    /// </summary>
    public void ResetAll()
    {
        // Reseting player's score.
        ScoreScript.Instance.ResetScore();

        // Emptying the list of activated decorations.
        activatedDecorations = new List<string>();

        // Re-initializing the dict of scenes that player has visited.
        SceneDictItem[] sceneDict = { new SceneDictItem("invaders", false), new SceneDictItem("360VideScene", false) };

        // Resetting visited scenes.
        List<string> tldrScenes = new List<string>();
        List<string> aarreScenes = new List<string>();
        List<string> otherScenes = new List<string>();

        // Transitioning back to Splash-scene.
        SceneManager.LoadScene("Splash");
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

