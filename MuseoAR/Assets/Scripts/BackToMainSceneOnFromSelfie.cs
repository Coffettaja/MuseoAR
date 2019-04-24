using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

///Simple script that adds onClick behavior to button for returning to the main scene
public class BackToMainSceneOnFromSelfie : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ReturnToMainSceneOnClick);
    }


    void ReturnToMainSceneOnClick()
    {

        //Check for if the coroutine has started

        StartCoroutine(WaitUntilDone());



        //if(!GameControllerScript.Instance.IsSceneCompleted(SceneManager.GetActiveScene().name))
        //{
        //    PlayerPrefs.SetString("DecorUnlock", SceneManager.GetActiveScene().name);
        //}
        //GameControllerScript.Instance.LoadTopLevelScene();
    }

    IEnumerator WaitUntilDone()
    {
        SelfieSceneInit init = GameObject.FindObjectOfType<SelfieSceneInit>();

        if (init != null)
        {
            init.ChangeToBackCamera();
        }


        yield return new WaitForSeconds(5);

        if (!GameControllerScript.Instance.IsSceneCompleted(SceneManager.GetActiveScene().name))
        {
            PlayerPrefs.SetString("DecorUnlock", SceneManager.GetActiveScene().name);
        }
        GameControllerScript.Instance.LoadTopLevelScene();
    }
}
