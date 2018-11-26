using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonScript : MonoBehaviour {

    private Button m_button;
    private GameObject m_infoPanel;
	// Use this for initialization
	void Start () {
        m_infoPanel = GameObject.Find("InfoPanel");
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ToggleInfoPanel);
        m_infoPanel.SetActive(false);
	}
	
	private void ToggleInfoPanel()
    {
        m_infoPanel.SetActive(!m_infoPanel.activeInHierarchy);
    }
}
