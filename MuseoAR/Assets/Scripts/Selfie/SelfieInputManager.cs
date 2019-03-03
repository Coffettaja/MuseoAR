using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SelfieInputManager : MonoBehaviour
{

    Camera cam;
    GameObject m_draggedObject;

    // Graphic Raycast members to be used 
    // when removing decorations
    GraphicRaycaster m_graphicRaycaster;
    PointerEventData m_pointerEventData;
    EventSystem m_eventSystem;

    [HideInInspector]
    public bool dragging;
    // This should only be used by DecorationListItem
    // when instantiating new decorations
    public GameObject DraggedObject
    {
        get { return m_draggedObject; }
        set
        {
            GameObject newDec = value;
            Vector3 pos = Input.mousePosition;

            pos.z = DECOR_Z_CORD;
            newDec.transform.position = cam.ScreenToWorldPoint(pos);
            m_draggedObject = newDec;
        }
    }
    const float DECOR_Z_CORD = 17.0f;
    Vector3 prevPos;
    Vector3 newPos;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();

        m_graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = DECOR_Z_CORD;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Decoration")
                {
                    m_draggedObject = hit.collider.gameObject;
                    prevPos = cam.ScreenToWorldPoint(mousePos);
                    dragging = true;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (m_draggedObject != null)
            {
                newPos = cam.ScreenToWorldPoint(mousePos);

                Vector3 translation = newPos - prevPos;
                m_draggedObject.transform.Translate(translation);

                prevPos = newPos;
            }
        }
        if (Input.GetMouseButtonUp(0)) // What to do on mouse button release.
        {
            m_pointerEventData = new PointerEventData(m_eventSystem);
            m_pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_graphicRaycaster.Raycast(m_pointerEventData, results);
            foreach(RaycastResult result in results)
            {
                if(result.gameObject.name == "RemoveZone")
                {
                    Destroy(m_draggedObject);
                }
            }

            dragging = false;
            m_draggedObject = null;
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            touchPos.z = DECOR_Z_CORD;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = cam.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.collider.gameObject.tag == "Decoration")
                        {
                            m_draggedObject = hit.collider.gameObject;
                            prevPos = cam.ScreenToWorldPoint(touchPos);
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if (m_draggedObject != null)
                    {

                        newPos = cam.ScreenToWorldPoint(touchPos);
                        Vector3 translation = newPos - prevPos;
                        m_draggedObject.transform.Translate(translation);

                        prevPos = newPos;
                    }
                    break;
                case TouchPhase.Ended:
                    m_draggedObject = null;
                    break;
            }
        }
#endif
    }
}
