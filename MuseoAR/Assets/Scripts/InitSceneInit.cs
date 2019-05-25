using UnityEngine;

/// <summary>
/// Changes the orientation of the screen.
/// </summary>
public class InitSceneInit : MonoBehaviour {

	void Start () {
    Screen.orientation = ScreenOrientation.LandscapeLeft;
  }
}
