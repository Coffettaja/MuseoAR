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


  [Header("Transition")]
  /// <summary>
  /// The image to use when transitioning to the scene.
  /// </summary>
  public Sprite transitionImage;
  /// <summary>
  /// The year to show during scene transition.
  /// </summary>
  public int transitionYear;
  public float transitionDuration;

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
	
  private void SetUpTransitionImage()
  {
    backgroundGameObject = new GameObject("Transition Image");
    spriteRenderer = backgroundGameObject.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = transitionImage;

    float cameraHeight = Camera.main.orthographicSize * 2;
    Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
    Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

    Vector2 scale = backgroundGameObject.transform.localScale;
    if (cameraSize.x >= cameraSize.y)
    { // Landscape (or equal)
      scale *= cameraSize.x / spriteSize.x;
    }
    else
    { // Portrait
      scale *= cameraSize.y / spriteSize.y;
    }

    backgroundGameObject.transform.position = Vector2.zero; // Optional
    backgroundGameObject.transform.localScale = scale;
  }

	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown("f") && transitionDuration > 0)
    {
      LoadScene();
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
      GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
      canvas.SetActive(false);
      StartCoroutine("TransitionToScene");
    }
    else
    {
      //GameControllerScript.Instance.LoadSceneWithName(sceneName);
    }
  }

  private IEnumerator TransitionToScene()
  {
    
    for (float f = 0; f < 1f; f += 1 / transitionDuration * Time.deltaTime)
    {
      // Move the the image on z-axis from 10 to 15 over a duration.
      backgroundGameObject.transform.position = new Vector3(0, 0, 10 + 5 * f);
      yield return null;
    }
    StartCoroutine("ShowYear");
    //GameControllerScript.Instance.LoadSceneWithName(sceneName);
  }

  private IEnumerator ShowYear()
  {
    for (float f = 0; f < 1f; f += 1 / transitionDuration * Time.deltaTime)
    {
      yield return null;
    }
    Debug.Log("Transitioning to scene: " + sceneName);
  }

  public TrackableBehaviour.Status getCurrent()
    {
        return _trackableBehaviour.CurrentStatus;
    }
}
