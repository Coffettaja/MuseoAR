using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadProfileScene : MonoBehaviour {

	private IEnumerator coroutine;
	
	void Start ()
	{
		coroutine = WaitAndLoad(5f);
		StartCoroutine(coroutine);
	}

    // Finds all obb-files and loads profileInput scene.
    private IEnumerator WaitAndLoad(float waitTime)
    {
        yield return StartCoroutine(ObbExtractor.ExtractObbDatasets(LoadNextScene));
		Debug.Log("Waiting for to load next Scene ");
		yield return new WaitForSeconds(0.5f);
       
	}
	void LoadNextScene() {
		Debug.Log("Starting to load next Scene ");
		SceneManager.LoadScene("profileInput");
	}
}
