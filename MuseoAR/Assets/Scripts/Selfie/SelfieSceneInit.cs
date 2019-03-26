using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// When running on android device, turns on the front camera when using loading the scene
/// and returning to back camera when returning to the main scene.
/// </summary>
public class SelfieSceneInit : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Debug.Log("Current camera direction: " + CameraDevice.Instance.GetCameraDirection());
        //        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        //ChangeToFrontCamera();
        //Invoke("ChangeToFrontCamera", 0.5f);
        Debug.Log("Current camera direction: " + CameraDevice.Instance.GetCameraDirection());
    Screen.orientation = ScreenOrientation.Portrait;
  }

  // Update is called once per frame
  void Update () {
		
	}

    public void ChangeToFrontCamera()
    {
        RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT);
    }

    public void ChangeToBackCamera()
    {
        RestartCamera(CameraDevice.CameraDirection.CAMERA_BACK);
    }

    private void RestartCamera(CameraDevice.CameraDirection newDir)
    {        
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        CameraDevice.Instance.Init(newDir);
        CameraDevice.Instance.Start();
    }
}
