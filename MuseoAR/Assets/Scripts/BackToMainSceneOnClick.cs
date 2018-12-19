using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///Simple script that adds onClick behavior to button for returning to the main scene
public class BackToMainSceneOnClick : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(ReturnToMainSceneOnClick);
	}

	void ReturnToMainSceneOnClick()
	{
        if(!GameControllerScript.Instance.IsSceneCompleted(SceneManager.GetActiveScene().name))
        {
            PlayerPrefs.SetString("DecorUnlock", SceneManager.GetActiveScene().name);
        }
		GameControllerScript.Instance.LoadTopLevelScene();
	}
}
