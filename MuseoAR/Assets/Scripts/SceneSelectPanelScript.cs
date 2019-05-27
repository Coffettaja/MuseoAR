using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectPanelScript : MonoBehaviour {
  public List<TrackableScript> normalTrackables;
  public List<TrackableScript> aarreTrackables;
  public List<TrackableScript> tldrTrackables;
  public GameObject sceneButtonPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < normalTrackables.Count; i++)
    {
      int j = i;
      CreateSceneButton(normalTrackables, j);
    }


	}

  private GameObject CreateButton(string name)
  {
    var button = Instantiate(sceneButtonPrefab, Vector3.zero, Quaternion.identity, transform);
    button.transform.localPosition = Vector3.zero;
    button.name = name + "Button";
    button.GetComponentInChildren<Text>().text =
      "Scene: " + name;
    return button;
  }

  private void CreateSceneButton(List<TrackableScript> trackables, int forTrackable)
  {
    var sceneButton = CreateButton(trackables[forTrackable].sceneName);
    sceneButton.GetComponent<Button>().onClick.AddListener(() => {
      transform.parent.gameObject.SetActive(false);
      trackables[forTrackable].LoadScene();
    });
  }
}
