using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// This script, along with a BoxCollider, has to be added to any ImageTarget that is used
/// to transition to another scene.
/// </summary>
public class TrackableScript : MonoBehaviour, ITrackableEventHandler {

  private TrackableBehaviour _trackableBehaviour;

  private SpriteRenderer spriteRenderer;
  private GameObject backgroundGameObject;

  /// <summary>
  /// The name of the scene to be launched when tracking the marker.
  /// </summary>
  public string sceneName;
  public string tldrIdentifier = "not_defined";
  public string aarre = "";


  [Header("Transition")]
  /// <summary>
  /// The image to use when transitioning to the scene.
  /// </summary>
  public Sprite transitionImage;

  public float transitionDuration;
  public float transitionBeginningZoomPercentage;

  // Use this for initialization
  void Start () {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        if(_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }

    SetUpTransitionImage();
    

    //GameControllerScript.gameManagerInstance = GameObject.Find("gameManager").GetComponent<GameControllerScript>();
  }

  private Vector2 scale;

  private void SetUpTransitionImage()
  {
    // Return if no image set.
    if (transitionImage == null) return;

    // Create the transition image object.
    backgroundGameObject = new GameObject("Transition Image");
    spriteRenderer = backgroundGameObject.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = transitionImage;

    // Set up the camera and scale, so the image fits on any screen size.
    float cameraHeight = Camera.main.orthographicSize * 2;
    Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
    Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

    this.scale = backgroundGameObject.transform.localScale;
    if (cameraSize.x >= cameraSize.y)
    { // Landscape (or equal)
      scale *= cameraSize.x / spriteSize.x;
    }
    else
    { // Portrait
      scale *= cameraSize.y / spriteSize.y;
    }

    backgroundGameObject.transform.position = Vector2.zero; // Just in case.
    backgroundGameObject.transform.localScale = scale;
  }

	// Update is called once per frame
	void Update () {
      // Used for debugging purposes.
    //if (Input.GetKeyDown("f") && transitionDuration > 0)
    //{
    //  LoadScene();
    //}
  }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.TRACKED)
        {

        }
        
    }

  public void LoadScene()
  {
    if (transitionDuration > 0)
    {
      // Set the UI canvas to inactive state, so the buttons cannot be pressed during the transition.
      GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
      //canvas.SetActive(false);
      StartCoroutine("TransitionToScene");
    }
    else if (aarre != "")
	{
	  //ScoreScript.Instance.IncreaseScoreBy(int)
	  Debug.Log("Aarre " + aarre);
	}
	else
	{
	  GameControllerScript.Instance.LoadSceneWithName(sceneName, tldrIdentifier);
	}
  }

  private IEnumerator TransitionToScene()
  {
    // Random value found by trying on different values on the editor...
    float scaleFactor = 7.9f * scale.x;  // Most likely will break with different image sizes. TODO FIX!
    for (float f = transitionBeginningZoomPercentage; f <= 1f; f += 1 / transitionDuration * Time.deltaTime)
    {
      // Move the the image on z-axis from 10 to 15 over a duration.
      // scale 1 = z 7.9 (for max view)
      backgroundGameObject.transform.position = new Vector3(0, 0, f * scaleFactor);
      yield return null;
    }
    yield return new WaitForSeconds(0.5f);
    GameControllerScript.Instance.LoadSceneWithName(sceneName);
  }

  public TrackableBehaviour.Status getCurrent()
    {
        return _trackableBehaviour.CurrentStatus;
    }
}
