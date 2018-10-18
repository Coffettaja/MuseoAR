using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInitScene : MonoBehaviour {

    private float _time = 0f;

	// Use this for initialization
	void Start () {
		
	}

    private IEnumerator WaitAndLoad(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
