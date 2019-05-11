using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZoneShowHide : MonoBehaviour {


    public GameObject RemoveZone;
    public float HideDistance;
    public float speed = 0.1f;

    public Vector3 m_showPosition;
    public Vector3 m_hidePosition;

    private bool m_dragging;
    private bool m_hidden = true;
    private SelfieInputManager inputManager;
	
    // Use this for initialization
	void Start ()
	{
	    RemoveZone = gameObject;
        m_hidePosition = RemoveZone.transform.position;
        m_showPosition = m_hidePosition + Vector3.right * HideDistance;

        inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
	}
	
	void Update () {
        m_dragging = inputManager.dragging;

    // If dragging an item and the trash can is hidden --> show the trash can.
        if(m_dragging && m_hidden)
        {
            StopAllCoroutines();
            StartCoroutine(Toggle(RemoveZone, m_showPosition, speed));
        }
        // If the trash can is shown, but an item is no longer dragger --> hide the trash can.
        else if(!m_dragging && !m_hidden)
        {
	        StopAllCoroutines();
		    StartCoroutine(Toggle(RemoveZone, m_hidePosition, speed));
	        
        }
    }

  // Shows or hides the "trash can" button.
    private IEnumerator Toggle(GameObject objectToMove, Vector3 destination, float speed)
    {
	    m_hidden = !m_hidden;
        while (objectToMove.transform.position != destination)
        {
	        //Debug.Log(objectToMove.transform.position);
            objectToMove.transform.position = Vector3.MoveTowards(
                objectToMove.transform.position, destination, Time.deltaTime * 100);
        yield return objectToMove.transform.position; 
        }
    }

}
