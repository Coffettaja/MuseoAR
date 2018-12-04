using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecotrationTrayToggle : MonoBehaviour {

    private GameObject m_decorationTray;

    private Vector3 m_trayDownPos;
    private Vector3 m_trayUpPos;

    public float moveSpeed = 0.5f;

    private bool m_trayUp;
	// Use this for initialization
	void Start () {
        
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ToggleTray);

        m_decorationTray = GameObject.Find("DecorationTray");
        RectTransform rt = m_decorationTray.GetComponent<RectTransform>();
        m_trayDownPos = m_decorationTray.transform.position;
        m_trayUpPos = m_trayDownPos + Vector3.up*rt.rect.height;

        m_trayUp = false;
	}
    
    private void ToggleTray()
    {
        if(m_trayUp)
        {
            StopAllCoroutines();
            StartCoroutine("MoveTraydown");
        }
        else if(!m_trayUp)
        {
            StopAllCoroutines();
            StartCoroutine("MoveTrayUp");
        }
    }

    private IEnumerator MoveTrayUp()
    {
        float startTime = Time.time;
        float endTime = startTime + moveSpeed;
        while(Time.time < endTime)
        {
            float t = (Time.time - startTime) / (endTime - startTime);
            m_decorationTray.transform.position = Vector3.Lerp(m_trayDownPos, m_trayUpPos, t);
            yield return null;
        }
        m_trayUp = true;
    }

    private IEnumerator MoveTraydown()
    {
        float startTime = Time.time;
        float endTime = startTime + moveSpeed;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / (endTime - startTime);
            m_decorationTray.transform.position = Vector3.Lerp(m_trayUpPos, m_trayDownPos, t);
            yield return null;
        }
        m_trayUp = false;
    }
}
