using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// This script, along with a BoxCollider, has to be added to any ImageTarget that is used
/// to transition to another scene.
/// </summary>
public class TrackableScript : MonoBehaviour, ITrackableEventHandler
{

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
    //public string loadSceneWithKey = "";

    [Header("Transition")]
    /// <summary>
    /// The image to use when transitioning to the scene.
    /// </summary>
    public Sprite transitionImage;

    public float transitionSpeed = 7;
    public float transitionBeginningZoom = 500;

    // Use this for initialization
    void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        SetUpTransitionImage();


        //GameControllerScript.gameManagerInstance = GameObject.Find("gameManager").GetComponent<GameControllerScript>();
    }

    private Vector2 scale;
    private UnityEngine.UI.Image img;
    private GameObject imgGO;

    private void SetUpTransitionImage()
    {
        if (transitionImage == null) return;

        imgGO = new GameObject();
        img = imgGO.AddComponent<UnityEngine.UI.Image>();
        imgGO.transform.parent = canvas.transform; // Transition image as the child of the canvas.
        imgGO.name = "TransitionImageFor" + gameObject.name;
        img.sprite = transitionImage;

        var rt = imgGO.transform as RectTransform;
        rt.localPosition = new Vector3(0, 0, -transitionBeginningZoom);
        rt.localScale = Vector3.one * 1.01f; // Just a tiny bit bigger just to make sure it fills the screen.
        rt.offsetMin = new Vector2(0, 0);
        rt.offsetMax = new Vector2(0, 0);
        imgGO.SetActive(false);
    }

    private void ScaleTransitionImage()
    {
        if (transitionImage == null) return;
        // Set up the camera and scale, so the image fits on any screen size.
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = transitionImage.bounds.size;
        float spriteAspectRatio = transitionImage.bounds.size.x / transitionImage.bounds.size.y;

        //Debug.Log(spriteAspectRatio);

        var rectTransform = ((RectTransform)img.transform);

        //Debug.Log("CameraHeight: " + cameraHeight);
        //Debug.Log("Aspect: " + Camera.main.aspect);

        // Landscape (or equal) --> Strech horizontally
        if (Camera.main.aspect >= spriteAspectRatio)
        {
            rectTransform.anchorMin = new Vector2(0, .5f);
            rectTransform.anchorMax = new Vector2(1f, .5f);
            rectTransform.pivot = new Vector2(.5f, .5f);
            rectTransform.sizeDelta = new Vector2(0, rectTransform.rect.width / spriteAspectRatio);
        }
        else
        { // Portrait --> Stretch vertically
            rectTransform.anchorMin = new Vector2(.5f, 0);
            rectTransform.anchorMax = new Vector2(.5f, 1f);
            rectTransform.pivot = new Vector2(.5f, .5f);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.height * spriteAspectRatio, 0);
        }
        //Debug.Log("Imgheight: " + rect.rect.height);
        //Debug.Log("Imgwidth: " + rect.rect.width);
        //Debug.Log("Width: " + rect.sizeDelta.x);
        //Debug.Log("Height: " + rect.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            if (transitionImage != null)
            {
                //StartSceneTransition();
            }
        }
    }

    private bool isTransitioning = false;

    private void StartSceneTransition()
    {
        if (isTransitioning) return;
        isTransitioning = true;
        //If the image is not selected, it cannot be used.
        if (imgGO != null)
        {
            imgGO.SetActive(true);

        }
        ScaleTransitionImage();
        ScaleTransitionImage(); // Called twice on purpose... otherwise it doesn't work and I can't be bothered to look into it now.
        StartCoroutine("TransitionToScene");
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.TRACKED)
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
            GameControllerScript.Instance.LoadSceneWithName(sceneName, tldrIdentifier, aarreIdentifier);
        }

        float z = -transitionBeginningZoom;
        while (z < 0)
        {
            z = z + transitionSpeed;
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
