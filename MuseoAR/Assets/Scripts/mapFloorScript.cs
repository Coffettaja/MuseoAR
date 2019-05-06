using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapFloorScript : MonoBehaviour {


    private Button m_button;
    private GameObject m_floorButton1;
    private GameObject m_floorButton2;
    private GameObject m_mapPanel;
    private GameObject m_mapPanel2;
    // Use this for initialization
    void Start()
    {
        m_mapPanel = GameObject.Find("MapPanel");
        m_mapPanel2 = GameObject.Find("MapPanel2");
        m_floorButton1 = GameObject.Find("MapButton_new1");
        m_floorButton2 = GameObject.Find("MapButton_new2");
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ChangeFloor);
    }

    // Deactivate the current floormap and floorbutton and activate the other two.
    private void ChangeFloor()
    {
        m_mapPanel.SetActive(!m_mapPanel.activeInHierarchy);
        m_mapPanel2.SetActive(!m_mapPanel.activeInHierarchy);
        m_floorButton1.SetActive(!m_floorButton1.activeInHierarchy);
        m_floorButton2.SetActive(!m_floorButton2.activeInHierarchy);
    }
}