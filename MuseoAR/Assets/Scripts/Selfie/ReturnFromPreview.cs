using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReturnFromPreview : MonoBehaviour {

  public GameObject screenshotPreviewPanel;
  public GameObject emailPanel;
  public TextMeshProUGUI emailInfo;

  public void ReturnFromPreviewHandler()
  {
    screenshotPreviewPanel.transform.localScale = Vector3.zero;
    emailPanel.transform.localScale = Vector3.zero;
    emailInfo.text = "";
  }
}
