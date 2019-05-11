using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvadersResetButton : MonoBehaviour {

    InvadersManagerScript m_manager;
    public GameObject managerParentObject;

    void Start () {
        m_manager = managerParentObject.GetComponent<InvadersManagerScript>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ResetScene);
	}
	
    void ResetScene()
    {
        m_manager.ResetGame();
    }
}
