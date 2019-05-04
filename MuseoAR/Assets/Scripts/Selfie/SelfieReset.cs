using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfieReset : MonoBehaviour {

  private GameObject canvas;
	// Use this for initialization
	void Start () {
    canvas = transform.parent.gameObject;
    gameObject.GetComponent<Button>().onClick.AddListener(ResetSelfie);
  }

  // Returns all the selfie items to their original position.
  public void ResetSelfie()
  {
    var itemsToReset = new List<ItemDragHandler>();
    foreach (Transform t in canvas.transform)
    {
      if (t.tag == "Decoration")
      {
        itemsToReset.Add(t.gameObject.GetComponent<ItemDragHandler>());
      }
    }

    // Immediately resetting the transform in the loop above doesn't work as expected,
    // so a list and another loop is used.
    foreach (var item in itemsToReset)
    {
      item.ResetTransform();
    }
  }
}
