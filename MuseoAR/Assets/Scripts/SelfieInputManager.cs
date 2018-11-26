using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelfieInputManager : MonoBehaviour
{

    Camera cam;
    GameObject m_draggedObject;
    //This should only be used by DecorationListItem
    //When instantiating new decorations
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

    public GameObject decorationPrefab;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
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
                    Debug.Log("Dragging " + m_draggedObject);
                    prevPos = cam.ScreenToWorldPoint(mousePos);
                    Debug.Log(prevPos);
                }
                //if (hit.collider.gameObject.tag == "DecorationList")
                //{
                //    Sprite sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                //    GameObject newDecoration = Instantiate<GameObject>(decorationPrefab);
                //    newDecoration.GetComponent<SpriteRenderer>().sprite = sprite;
                //    draggedObject = newDecoration;
                //}
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
        if (Input.GetMouseButtonUp(0))
        {
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
