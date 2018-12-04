using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZoneShowHide : MonoBehaviour {


    public GameObject RemoveZone;
    public float HideDistance;
    public float speed = 0.5f;

    private Vector3 m_showPosition;
    private Vector3 m_hidePosition;

    private bool m_dragging;
    private bool m_hidden;
    private SelfieInputManager inputManager;
	
    // Use this for initialization
	void Start () {
        m_showPosition = RemoveZone.transform.position;
        m_hidePosition = m_showPosition + Vector3.left * HideDistance;

        inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
	}
	
	void Update () {
        m_dragging = inputManager.dragging;
        if(m_dragging)
        {
            if(m_hidden)
            {
                StopCoroutine("Toggle");
                StartCoroutine(Toggle(RemoveZone, m_showPosition, speed));
            }
            else
            {
                StopCoroutine("Toggle");
                StartCoroutine(Toggle(RemoveZone, m_hidePosition, speed));
            }
        }
    }

    private IEnumerator Toggle(GameObject objectToMove, Vector3 destination, float speed)
    {
        while (RemoveZone.transform.position != destination)
        {
            objectToMove.transform.position = Vector3.MoveTowards(
                objectToMove.transform.position, destination, Time.deltaTime * speed);
        yield return objectToMove.transform.position; 
        }
    }

}
