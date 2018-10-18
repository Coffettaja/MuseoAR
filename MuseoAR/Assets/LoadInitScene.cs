using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadInitScene : MonoBehaviour {

    private float _time = 0f;

	private IEnumerator coroutine;
	
	// Use this for initialization
	void Start ()
	{
		coroutine = WaitAndLoad(3.0f);
		StartCoroutine(coroutine);
	}

    private IEnumerator WaitAndLoad(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("init");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
