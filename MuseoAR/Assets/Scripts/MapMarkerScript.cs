using System.Collections.Generic;
using UnityEngine;

public class MapMarkerScript : MonoBehaviour {

  public List<GameObject> floors;
  private List<GameObject> markers = new List<GameObject>();

	// Use this for initialization
	void Start ()
  {
    SetUpMarkers();
    ActivateMarkers();
  }

  /// <summary>
  /// Get all the marker GameObjects from the floors.
  /// </summary>
  private void SetUpMarkers()
  {
    foreach (var floor in floors)
    {
      foreach (Transform child in floor.transform)
      {
        markers.Add(child.gameObject);
      }
    }
  }

  /// <summary>
  /// Activate the markers that should be activated.
  /// </summary>
  private void ActivateMarkers()
  {
    List<string> scenes = new List<string>();

    // Why is there no Select in .NET 2.0 :'(
    foreach (var scene in GameControllerScript.tldrScenes)
    {
      scenes.Add("tldr" + scene);
    }

    foreach (var scene in GameControllerScript.aarreScenes)
    {
      scenes.Add("aarre" + scene);
    }

    scenes.AddRange(GameControllerScript.otherScenes);

    foreach (var scene in scenes)
    {
      markers.Find(marker => marker.name == scene).SetActive(true);
    }
  }

    /// <summary>
    /// Deactivate the markers that have been sctivated.
    /// </summary>
    public void DeactivateMarkers()
    {
        SetUpMarkers();
        List<string> scenes = new List<string>();

        foreach (var scene in GameControllerScript.tldrScenes)
        {
            scenes.Add("tldr" + scene);
        }

        foreach (var scene in GameControllerScript.aarreScenes)
        {
            scenes.Add("aarre" + scene);
        }

        scenes.AddRange(GameControllerScript.otherScenes);

        foreach (var scene in scenes)
        {
            markers.Find(marker => marker.name == scene).SetActive(false);
        }
    }
    private static object _lock = new object();

    #region Singleton creation
    private static MapMarkerScript _instance;
    public static MapMarkerScript Instance
    {
        get
        {
            // Locks down the thread until the the Singleton instance has been created.
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (MapMarkerScript)FindObjectOfType(
                        typeof(MapMarkerScript));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<MapMarkerScript>();
                        singletonObject.name = typeof(MapMarkerScript).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }
            return _instance;
        }
    }
    #endregion
}
