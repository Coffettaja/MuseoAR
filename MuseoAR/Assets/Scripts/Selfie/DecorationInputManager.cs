using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecorationInputManager : MonoBehaviour, IDragHandler, IEndDragHandler {
  public void OnDrag(PointerEventData eventData)
  {
    transform.position = Input.mousePosition;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    
  }

  // Use this for initialization
  void Start () {
    var item = gameObject.GetComponent<DecorationListItem>();
    //Debug.Log(item.IsDecorationActive());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
