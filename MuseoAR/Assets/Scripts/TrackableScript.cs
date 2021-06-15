using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;


/// <summary>
/// This script, along with a BoxCollider, has to be added to any ImageTarget that is used
/// to transition to another scene.
/// </summary>
public class TrackableScript : DefaultTrackableEventHandler { //, ITrackableEventHandler

  private TrackableBehaviour _trackableBehaviour;
    private System.Action<TrackableBehaviour.StatusChangeResult> OnTrackableState;
    private GameObject canvas;

  /// <summary>
  /// The name of the scene to be launched when tracking the marker.
  /// </summary>
  public string sceneName;
  public string tldrIdentifier = "";
  public string aarreIdentifier = "";

  // Used for debugging purposes.
  //public string loadSceneWithKey = "";

  [Header("Transition")]
  /// <summary>
  /// The image to use when transitioning to the scene.
  /// </summary>
  public Sprite transitionImage;

  public float transitionSpeed = 7;
  public float transitionBeginningZoom = 500;

  // Use this for initialization
  void Start () {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        OnTrackableState = OnTrackableStateChanged;
        if (_trackableBehaviour) {
            _trackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableState);
        }

        canvas = GameObject.FindGameObjectWithTag("Canvas");
    SetUpTransitionImage();
    

    //GameControllerScript.gameManagerInstance = GameObject.Find("gameManager").GetComponent<GameControllerScript>();
  }

  private Vector2 scale;
  private UnityEngine.UI.Image img;
  private GameObject imgGO;
  private Button imgButton;

  private void SetUpTransitionImage()
  {
    if (transitionImage == null) return;

    imgGO = new GameObject();
    img = imgGO.AddComponent<UnityEngine.UI.Image>();
    imgButton = imgGO.AddComponent<Button>();

    imgGO.transform.SetParent(canvas.transform); // Transition image as the child of the canvas.
    imgGO.name = "TransitionImageFor" + gameObject.name;
    img.sprite = transitionImage;

    var rt = imgGO.transform as RectTransform;
    rt.localPosition = new Vector3(0, 0, -transitionBeginningZoom);
    rt.localScale = Vector3.one * 1.05f; // Just a tiny bit bigger just to make sure it fills the screen.
    rt.offsetMin = new Vector2(0, 0);
    rt.offsetMax = new Vector2(0, 0);
    ScaleImage();
    imgGO.SetActive(false);
  }

  // Sets the transition image to fit the screen (when at pos 0, 0, 0)
  private void ScaleImage()
  {
    var fitter = imgGO.AddComponent<AspectRatioFitter>();
    fitter.aspectRatio = transitionImage.bounds.size.x / transitionImage.bounds.size.y;
    fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
  }

  private bool isTransitioning = false;

  private void StartSceneTransition()
  {
    if (isTransitioning) return;
    isTransitioning = true;
    if (transitionImage != null)
    {
      imgGO.SetActive(true);
    }
      StartCoroutine("TransitionToScene");
  }

    public void OnTrackableStateChanged(TrackableBehaviour.StatusChangeResult change) {
        if(change.NewStatus == TrackableBehaviour.Status.TRACKED)
        {
        
        }
        
    }

  public void LoadScene()
  {
    StartSceneTransition();
  }

  private IEnumerator TransitionToScene()
  {
    // Load the scene instantly if no transition image is set.
    if (transitionImage == null || transitionSpeed <= 0)
    {
      Debug.Log("No transition image");
      GameControllerScript.Instance.LoadSceneWithName(sceneName, tldrIdentifier, aarreIdentifier);
      yield break;
    }

    float z = transitionBeginningZoom;
    // Transition image effect can be finished instantly by clicking on it.
    imgButton.onClick.AddListener(() => z = 1); 
    while (z > 0)
    {
      z = z - transitionSpeed;
      imgGO.transform.localPosition = new Vector3(0, 0, z);
      yield return null;
    }
  
    yield return new WaitForSeconds(0.5f);
    GameControllerScript.Instance.LoadSceneWithName(sceneName, tldrIdentifier, aarreIdentifier);
  }

  public TrackableBehaviour.Status getCurrent()
    {
        return _trackableBehaviour.CurrentStatus;
    }
}
