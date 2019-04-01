using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationListInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var activatedDecorations = GameControllerScript.Instance.GetActivatedDecorations();
    foreach (string itemToActivate in activatedDecorations)
    {
      //Debug.Log(itemToActivate);
      Transform childTransform = transform.Find(itemToActivate);
      if (childTransform != null)
      {
        childTransform.GetComponent<DecorationListItem>().IsActive(true);
      }
    }
  }

  // Update is called once per frame
  void Update () {
		
	}
}
