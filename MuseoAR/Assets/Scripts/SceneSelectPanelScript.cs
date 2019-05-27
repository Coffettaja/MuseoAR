using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelectPanelScript : MonoBehaviour {
  public List<TrackableScript> normalTrackables;
  public List<TrackableScript> tldrTrackables;
  public GameObject aarteet;
  public GameObject sceneButtonPrefab;

  public GameObject defaultPanel;
  public GameObject aarrePanel;
  public GameObject tldrPanel;

  private List<TrackableScript> aarreTrackables = new List<TrackableScript>();

	// Use this for initialization
	void Start () {
    tldrPanel.SetActive(false);
    aarrePanel.SetActive(false);
    foreach (Transform aarre in aarteet.transform)
    {
      aarreTrackables.Add(aarre.gameObject.GetComponent<TrackableScript>());
    }
    InitMainPanel();
    InitTldrPanel();
    InitAarrePanel();
	}

  private void InitMainPanel()
  {
    for (int i = 0; i < normalTrackables.Count; i++)
    {
      int j = i;
      CreateSceneButton(normalTrackables, j, defaultPanel.transform);
    }
    var tldrButton = CreateButton("tldr", defaultPanel.transform);
    tldrButton.GetComponent<Image>().color = Color.cyan;
    tldrButton.onClick.AddListener(ShowTldrPanel);

    var aarreButton = CreateButton("aarre", defaultPanel.transform);
    aarreButton.GetComponent<Image>().color = Color.magenta;
    aarreButton.onClick.AddListener(ShowAarrePanel);
  }

  private void InitTldrPanel()
  {
    for (int i = 0; i < tldrTrackables.Count; i++)
    {
      int j = i;
      CreateSceneButton(tldrTrackables, j, tldrPanel.transform, (j + 1).ToString());
    }
    var returnButton = CreateButton("Return", tldrPanel.transform);
    returnButton.GetComponent<Image>().color = Color.yellow;
    returnButton.onClick.AddListener(ShowMainPanel);
  }

  private void InitAarrePanel()
  {
    for (int i = 0; i < aarreTrackables.Count - 13; i++)
    {
      int j = i;
      CreateSceneButton(aarreTrackables, j, aarrePanel.transform, (j + 1).ToString());
    }
    var returnButton = CreateButton("Return", aarrePanel.transform);
    returnButton.GetComponent<Image>().color = Color.yellow;
    returnButton.onClick.AddListener(ShowMainPanel);
  }

  private void ShowMainPanel()
  {
    tldrPanel.SetActive(false);
    aarrePanel.SetActive(false);
  }

  private void ShowTldrPanel()
  {
    tldrPanel.SetActive(true);
  }

  private void ShowAarrePanel()
  {
    aarrePanel.SetActive(true);
  }

  private Button CreateButton(string name, Transform parent, string append = "")
  {
    var button = Instantiate(sceneButtonPrefab, Vector3.zero, Quaternion.identity, parent);
    button.transform.localPosition = Vector3.zero;
    button.name = name + append + "Button";
    button.GetComponentInChildren<Text>().text =
      "Scene: " + name + append;
    return button.GetComponent<Button>();
  }

  private void CreateSceneButton(List<TrackableScript> trackables, int forTrackable, Transform parent, string append = "")
  {
    var sceneButton = CreateButton(trackables[forTrackable].sceneName, parent, append);
    sceneButton.onClick.AddListener(() => {
      transform.parent.gameObject.SetActive(false);
      trackables[forTrackable].LoadScene();
    });
  }
}
