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

    // Get all the aarre markers from the aarteet and add them to the list.
    foreach (Transform aarre in aarteet.transform)
    {
      aarreTrackables.Add(aarre.gameObject.GetComponent<TrackableScript>());
    }
    InitMainPanel();
    InitTldrPanel();
    InitAarrePanel();
	}

  /// <summary>
  /// Creates and sets the buttons and their behaviors for the main panel.
  /// </summary>
  private void InitMainPanel()
  {
    for (int i = 0; i < normalTrackables.Count; i++)
    {
      int j = i; // Need to do this to catch the value so the button activates the correct scene.
      CreateSceneButton(normalTrackables, j, defaultPanel.transform);
    }

    var tldrButton = CreateButton("tldr", defaultPanel.transform);
    tldrButton.GetComponent<Image>().color = Color.cyan;
    tldrButton.onClick.AddListener(ShowTldrPanel);

    var aarreButton = CreateButton("aarre", defaultPanel.transform);
    aarreButton.GetComponent<Image>().color = Color.magenta;
    aarreButton.onClick.AddListener(ShowAarrePanel);
  }

  /// <summary>
  /// Creates and sets the buttons and their behaviors for the tldr panel.
  /// </summary>
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

  /// <summary>
  /// Creates and sets the buttons and their behaviors for the aarre panel.
  /// </summary>
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

  /// <summary>
  /// Creates a new button from the sceneButtonPrefab, and childs it to the given parent transform.
  /// </summary>
  /// <param name="name">Button name</param>
  /// <param name="parent">Parent transform for the created button</param>
  /// <param name="append">Optionally add some text to the button name and text.</param>
  /// <returns></returns>
  private Button CreateButton(string name, Transform parent, string append = "")
  {
    // Create the button and set its position, name and inner text.
    var button = Instantiate(sceneButtonPrefab, Vector3.zero, Quaternion.identity, parent);
    button.transform.localPosition = Vector3.zero;
    button.name = name + append + "Button";
    button.GetComponentInChildren<Text>().text =
      "Scene: " + name + append;

    return button.GetComponent<Button>();
  }

  /// <summary>
  /// Creates a button that activates the scene transition for trackable given.
  /// </summary>
  /// <param name="trackables">List of trackableScripts from where to choose the scene.</param>
  /// <param name="forTrackable">Index of the trackableScript that activates on click.</param>
  /// <param name="parent">Parent for the created button.</param>
  /// <param name="append">Optional text to be added to the name and text of the button.</param>
  private void CreateSceneButton(List<TrackableScript> trackables, int forTrackable, Transform parent, string append = "")
  {
    var sceneButton = CreateButton(trackables[forTrackable].sceneName, parent, append);

    // Load the scene tied to the trackableScript on button click.
    sceneButton.onClick.AddListener(() => {
      transform.parent.gameObject.SetActive(false);
      trackables[forTrackable].LoadScene();
    });
  }
}
