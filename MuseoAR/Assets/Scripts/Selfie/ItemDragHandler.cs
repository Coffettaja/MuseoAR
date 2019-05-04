using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

  private RectTransform rectTransform;
  private RectTransform itemPanel;
  private RectTransform canvas;
  private RectTransform itemSlot;
  private DecorationListItem item;

  public ItemInputHandler inputHandler;

  //private bool isOnCanvas = false;

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (inputHandler.Dragging) return;
    inputHandler.Dragging = true;
    if (!item.DecorationActive())
    {
      Debug.Log(item.name + " not active!");
      return;
    };

    // Basically sets the item to be the front-most element after buttons.
    transform.SetSiblingIndex(canvas.childCount - 5);
    inputHandler.SetRectTransform(rectTransform);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (inputHandler.GetDraggedRectTransform() != rectTransform) return;
    if (!item.DecorationActive())
    {
      Debug.Log(item.name + " not active!");
      return;
    };

#if UNITY_EDITOR
    transform.position = Input.mousePosition;
#elif UNITY_ANDROID
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
      Vector3 touchPos = touch.position;
      transform.position = touchPos;
    }
#endif

  }

  public void OnEndDrag(PointerEventData eventData)
  {
    inputHandler.Dragging = false;
    // Check if the current mouse coordinates are inside the panel
    if (RectTransformUtility.RectangleContainsScreenPoint(itemPanel, Input.mousePosition))
    {
      ResetTransform();
    }
    else
    {
      transform.SetParent(canvas);
      // Basically sets the item to be the front-most element after buttons.
      transform.SetSiblingIndex(canvas.childCount - 5);

      // Need this for consistent behaviour for activating items through both dragging and tapping.
      gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.None;
    }
  }

  public void ResetTransform()
  {
    transform.SetParent(itemSlot);
    rectTransform.anchoredPosition = Vector2.zero;
    rectTransform.rotation = Quaternion.identity;
    rectTransform.localScale = Vector3.one;
    gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
  }

  // Use this for initialization
  void Start() {
    rectTransform = gameObject.GetComponent<RectTransform>();
    itemSlot = transform.parent as RectTransform;
    itemPanel = GameObject.FindWithTag("Inventory").transform as RectTransform;
    canvas = GameObject.FindGameObjectWithTag("Canvas").transform as RectTransform;
    item = transform.parent.GetComponent<DecorationListItem>();
  }
}
