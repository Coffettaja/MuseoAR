using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectButton : MonoBehaviour {
  public GameObject sceneSelectPanel;
  public GameObject infoPanel;

  public void ToggleSceneSelectPanel()
  {
    sceneSelectPanel.SetActive(!sceneSelectPanel.activeSelf);
  }
}
