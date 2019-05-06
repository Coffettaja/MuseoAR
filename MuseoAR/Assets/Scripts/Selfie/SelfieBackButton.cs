using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfieBackButton : MonoBehaviour {
  public void BackToInitScene()
  {
    StartCoroutine("ChangeScene");
    
  }

  private IEnumerator ChangeScene()
  {
    yield return new WaitForEndOfFrame();
    Screen.orientation = ScreenOrientation.Portrait;
    yield return new WaitForEndOfFrame();
    GameControllerScript.Instance.LoadTopLevelScene();
  }
}
