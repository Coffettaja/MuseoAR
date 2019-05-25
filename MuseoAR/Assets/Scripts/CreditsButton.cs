using UnityEngine;

public class CreditsButton : MonoBehaviour {
  public GameObject creditsPanel;

	
  public void ToggleCreditsPanel()
  {
    creditsPanel.SetActive(!creditsPanel.activeSelf);
  }
}
