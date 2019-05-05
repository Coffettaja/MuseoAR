using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFromPreview : MonoBehaviour {

  public GameObject screenshotPreviewPanel;

  public void ReturnFromPreviewHandler()
  {
    screenshotPreviewPanel.transform.localScale = Vector3.zero;
  }
}
