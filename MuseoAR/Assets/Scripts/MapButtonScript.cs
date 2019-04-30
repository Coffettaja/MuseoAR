using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonScript : MonoBehaviour {

    private Button m_button;
    private GameObject m_floorButton1;
    private GameObject m_floorButton2;
    private GameObject m_mapPanel;
    private GameObject m_mapPanel2;
    private bool flag;

    // Use this for initialization
    void Start () {
        m_mapPanel = GameObject.Find("MapPanel");
        m_mapPanel2 = GameObject.Find("MapPanel2");
        m_floorButton1 = GameObject.Find("MapButton_new1");
        m_floorButton2 = GameObject.Find("MapButton_new2");
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ToggleMap);
        m_mapPanel.SetActive(false);
        m_mapPanel2.SetActive(false);
        m_floorButton1.SetActive(false);
        m_floorButton2.SetActive(false);
        flag = true;
    }
	
	private void ToggleMap()
    {
        // Activate the first floor map and the button of the second floor map.
        if (flag)
        {
            m_mapPanel.SetActive(!m_mapPanel.activeInHierarchy);
            m_floorButton2.SetActive(!m_floorButton2.activeInHierarchy);
            flag = false;
        }
        // Deactivate all maps and floor buttons.
        else
        {
            m_mapPanel.SetActive(false);
            m_mapPanel2.SetActive(false);
            m_floorButton1.SetActive(false);
            m_floorButton2.SetActive(false);
            flag = true;
        }
    }
}
