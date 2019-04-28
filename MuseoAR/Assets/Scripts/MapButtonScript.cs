using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonScript : MonoBehaviour {

    private Button m_button;
    private GameObject m_mapPanel;

  //public MapGestureManager inputHandler;

  // Use this for initialization
  void Start () {
        m_mapPanel = GameObject.Find("MapPanel");
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ToggleInfoPanel);
        m_mapPanel.SetActive(false);
	}
	
	private void ToggleInfoPanel()
    {
        m_mapPanel.SetActive(!m_mapPanel.activeInHierarchy);

    //if (m_mapPanel.activeSelf)
    //{
    //  inputHandler.SetupGestures();
    //}
    }
}
