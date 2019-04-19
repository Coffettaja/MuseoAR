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
  private GameObject canvas;

  /// <summary>
  /// The name of the scene to be launched when tracking the marker.
  /// </summary>
  public string sceneName;
  public string tldrIdentifier = "not_defined";
  public string aarreIdentifier = "not_defined";

  // Used for debugging purposes.
  public string loadSceneWithKey = "";

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

    canvas = GameObject.FindGameObjectWithTag("Canvas");
    SetUpTransitionImage();
    

    //GameControllerScript.gameManagerInstance = GameObject.Find("gameManager").GetComponent<GameControllerScript>();
  }

  private Vector2 scale;
  private UnityEngine.UI.Image img;

  private void SetUpTransitionImage()
  {
    if (transitionImage == null) return;

    var imgGO = new GameObject();
    img = imgGO.AddComponent<UnityEngine.UI.Image>();
    imgGO.transform.parent = canvas.transform;
    //img.preserveAspect = true;
    img.name = "TransitionImageFor" + gameObject.name;
    img.sprite = transitionImage;

    var rt = imgGO.transform as RectTransform;
    rt.localPosition = Vector3.zero;
    rt.localScale = Vector3.one * 1.01f;
    rt.offsetMin = new Vector2(0, 0);
    rt.offsetMax = new Vector2(0, 0);


  }
  
  private void ScaleTransitionImage()
  {
    if (transitionImage == null) return;
    // Set up the camera and scale, so the image fits on any screen size.
    float cameraHeight = Camera.main.orthographicSize * 2;
    Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
    Vector2 spriteSize = transitionImage.bounds.size;
    float spriteAspectRatio = transitionImage.bounds.size.x / transitionImage.bounds.size.y;

    Debug.Log(spriteAspectRatio);

    var rect = ((RectTransform)img.transform);

    Debug.Log("CameraHeight: " + cameraHeight);
    Debug.Log("Aspect: " + Camera.main.aspect);
   

    if (Camera.main.aspect >= spriteAspectRatio)
    { // Landscape (or equal) --> Strech horizontally
      rect.anchorMin = new Vector2(0, .5f);
      rect.anchorMax = new Vector2(1f, .5f);
      rect.pivot = new Vector2(.5f, .5f);
      Debug.Log("Prev" + rect.sizeDelta.x);
      rect.sizeDelta = new Vector2(0, rect.rect.width / spriteAspectRatio);
    }
    else
    { // Portrait --> Stretch vertically
      rect.anchorMin = new Vector2(.5f, 0);
      rect.anchorMax = new Vector2(.5f, 1f);
      rect.pivot = new Vector2(.5f, .5f);
      rect.sizeDelta = new Vector2(rect.rect.height * spriteAspectRatio, 0);
    }
    Debug.Log("Imgheight: " + rect.rect.height);
    Debug.Log("Imgwidth: " + rect.rect.width);
    Debug.Log("Width: " + rect.sizeDelta.x);
      Debug.Log("Height: " + rect.sizeDelta.y);
  }

	// Update is called once per frame
	void Update () {
      // Used for debugging purposes.
    if (loadSceneWithKey != "" && Input.GetKeyDown(loadSceneWithKey) && transitionDuration > 0)
    {
      LoadScene();
    }

    if (Input.GetKeyDown("o"))
    {
      ScaleTransitionImage();
      ScaleTransitionImage(); // Obviously have to call it twice in a row for it to work properly...
    }
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
      //canvas.SetActive(false);
      StartCoroutine("TransitionToScene");
    }
    
	else
	{
      // Send marker identifiers as parameters along with scenename to game controller.
	  GameControllerScript.Instance.LoadSceneWithName(sceneName, tldrIdentifier, aarreIdentifier);
	}
  }

  private IEnumerator TransitionToScene()
  {
    //Camera.main.transform.position
    // Random value found by trying on different values on the editor...
    //backgroundGameObject.SetActive(true);
    float scaleFactor = 7.9f * scale.x;  // Most likely will break with different image sizes. TODO FIX!
    float xPos = backgroundGameObject.transform.position.x;
    float yPos = backgroundGameObject.transform.position.y;
    for (float f = transitionBeginningZoomPercentage; f <= 1f; f += 1 / transitionDuration * Time.deltaTime)
    {
      // Move the the image on z-axis from 10 to 15 over a duration.
      // scale 1 = z 7.9 (for max view)
      backgroundGameObject.transform.position = new Vector3(xPos, yPos, f * scaleFactor);
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
