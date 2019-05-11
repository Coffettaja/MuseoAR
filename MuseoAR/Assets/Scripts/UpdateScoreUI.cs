using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Updates the UI for the score display.
/// </summary>
public class UpdateScoreUI : MonoBehaviour {

  // The current score value is loaded when the score UI is loaded.
  // Currently that happens when loading the main scene.
  private void Awake()
  {
    ScoreScript.Instance.UpdateScoreDisplay();
  }
}
