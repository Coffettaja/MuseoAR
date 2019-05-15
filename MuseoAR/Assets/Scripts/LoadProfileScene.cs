using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Is this script ever used?
public class LoadProfileScene : MonoBehaviour {

    private float _time = 0f;

	private IEnumerator coroutine;
	
	// Use this for initialization
	void Start ()
	{
		coroutine = WaitAndLoad(2.0f);
		StartCoroutine(coroutine);
	}

    private IEnumerator WaitAndLoad(float waitTime)
    {
        yield return StartCoroutine(ObbExtractor.ExtractObbDatasets());
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("profileInput");
	}
}
