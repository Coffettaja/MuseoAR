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
    private GameObject asd;
    private bool flag;
    private List<GameObject> marks = new List<GameObject>();

    // Use this for initialization
    void Start () {
        m_mapPanel = GameObject.Find("MapPanel");
        m_mapPanel2 = GameObject.Find("MapPanel2");
        m_floorButton1 = GameObject.Find("MapButton_new1");
        m_floorButton2 = GameObject.Find("MapButton_new2");
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(ToggleMap);
        AddMarks();
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
            ActivateMarks();
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

    private void AddMarks()
    {
        GameObject tldr1;
        tldr1 = GameObject.Find("MapPanel/Image/tldr0");
        tldr1.SetActive(false);
        marks.Add(tldr1);

        GameObject tldr2;
        tldr2 = GameObject.Find("MapPanel/Image/tldr1");
        tldr2.SetActive(false);
        marks.Add(tldr2);
    }

#region activatemarks
    private void ActivateMarks()
    {
        List<string> tldrScenes = GameControllerScript.tldrScenes;
        List<string> aarreScenes = GameControllerScript.aarreScenes;
        List<string> otherScenes = GameControllerScript.otherScenes;

        foreach (var tldr in tldrScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;
                tag = tag.Substring(tag.Length - 1);

                if (tldr == tag)
                {
                    mark.SetActive(true);
                }
            }
        }

        foreach (var aarre in aarreScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;

                if (aarre == tag)
                {
                    mark.SetActive(true);
                }
            }
        }

        foreach (var other in otherScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;

                if (other == tag)
                {
                    mark.SetActive(true);
                }
            }
        }
    }
#endregion
}
