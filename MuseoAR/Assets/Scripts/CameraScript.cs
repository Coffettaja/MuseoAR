using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	private void Start () {
        Application.targetFrameRate = 240;
	}

    private void Update()
    {
        transform.rotation = Input.gyro.attitude;
        Debug.Log(Input.gyro.attitude);
    }

}
