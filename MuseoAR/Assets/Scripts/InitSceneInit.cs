using UnityEngine;

/// <summary>
/// Changes the orientation of the screen.
/// </summary>
public class InitSceneInit : MonoBehaviour {

	void Start () {
    Screen.autorotateToLandscapeRight = false;
    Screen.autorotateToPortraitUpsideDown = false;
    Screen.autorotateToPortrait = false;
    Screen.autorotateToLandscapeLeft = true;
  }
}
