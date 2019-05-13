using System.Collections;
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
}
