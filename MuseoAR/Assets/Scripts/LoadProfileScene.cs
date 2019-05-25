using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadProfileScene : MonoBehaviour {

	private IEnumerator coroutine;
	
	void Start ()
	{
		coroutine = WaitAndLoad(2.0f);
		StartCoroutine(coroutine);
	}

    // Finds all obb-files and loads profileInput scene.
    private IEnumerator WaitAndLoad(float waitTime)
    {
        yield return StartCoroutine(ObbExtractor.ExtractObbDatasets());
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("profileInput");
	}
}
