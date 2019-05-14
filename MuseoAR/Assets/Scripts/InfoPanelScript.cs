using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Debug.Log("HMM?: " + SceneManager.GetActiveScene().name);
    if (GameControllerScript.Instance.IsSceneCompleted(SceneManager.GetActiveScene().name))
    {
      SetPanelActive(false);
    }
	}
	
	public void SetPanelActive(bool active)
  {
    gameObject.SetActive(active);
  }
}
