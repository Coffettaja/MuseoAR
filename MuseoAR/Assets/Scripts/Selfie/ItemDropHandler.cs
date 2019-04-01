using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {
  public void OnDrop(PointerEventData eventData)
  {
    RectTransform panel = transform as RectTransform;

    // Check if the current mouse coordinates are inside the panel
    if (RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition))
    {
      //Debug.Log("Item on canvas");
    }
  }
}
