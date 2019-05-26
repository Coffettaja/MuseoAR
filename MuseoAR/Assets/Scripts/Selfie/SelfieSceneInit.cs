using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// When running on android device, turns on the front camera when using loading the scene
/// and returning to back camera when returning to the main scene.
/// </summary>
public class SelfieSceneInit : MonoBehaviour {

  public RawImage background;
  public AspectRatioFitter fitter;

  private bool camAvailable;
  private Texture defaultBackground;

  private WebCamTexture backCam;
  private WebCamTexture frontCam;
  private WebCamTexture camTexture;

  private bool usingFrontCamera;

  // Use this for initialization
  void Start () {
    Application.RequestUserAuthorization(UserAuthorization.WebCam);
    VuforiaBehaviour.Instance.enabled = false;
    Screen.autorotateToPortrait = true;
    defaultBackground = background.texture;

    WebCamDevice[] devices = WebCamTexture.devices;

    if (devices.Length == 0)
    {
      Debug.Log("No camera");
      camAvailable = false;
      return;
    }
    
    for (int i = 0; i < devices.Length; i++)
    {
      if (devices[i].isFrontFacing)
      {
        //frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
        frontCam = new WebCamTexture(devices[i].name);
        frontCam.requestedWidth = Screen.width;
        frontCam.requestedHeight = Screen.height;
      }
      else
      {
        //backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
        backCam = new WebCamTexture(devices[i].name);
        backCam.requestedWidth = Screen.width;
        backCam.requestedHeight = Screen.height;
      }
    }

    if (frontCam == null && backCam == null)
    {
      Debug.Log("No camera found");
      return;
    }

    UseFrontCamera();

    camAvailable = true;
  }

  private void Update()
  {
    if (!camAvailable) return;

    float ratio = (float)camTexture.width / (float)camTexture.height;
    fitter.aspectRatio = ratio;

    float scaleY = camTexture.videoVerticallyMirrored ? -1f : 1f;
    background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

    int orient = -camTexture.videoRotationAngle;
    background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
  }

  private void UseFrontCamera()
  {
    if (frontCam == null) return;
    if (camTexture != null && camTexture.isPlaying)
    {
      camTexture.Stop();
    }
    camTexture = frontCam;
    camTexture.Play();
    background.texture = camTexture;
    usingFrontCamera = true;
  }

  private void UseBackCamera()
  {
    if (backCam == null) return;
    if (camTexture != null && camTexture.isPlaying)
    {
      camTexture.Stop();
    }
    camTexture = backCam;
    camTexture.Play();
    background.texture = camTexture;
    usingFrontCamera = false;
  }

  public void ToggleCamera()
  {
    if (usingFrontCamera)
    {
      UseBackCamera();
    } else
    {
      UseFrontCamera();
    }
  }
}
