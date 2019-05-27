using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Checks if the scene has been visited previously. If not, displays info-message.
/// </summary>
public class InfoPanelScript : MonoBehaviour {

	void Start () {
    if (GameControllerScript.Instance.IsSceneCompleted(SceneManager.GetActiveScene().name))
    {
      SetPanelActive(false);
    } else
    {
      GameControllerScript.Instance.MarkSceneCompleted(SceneManager.GetActiveScene().name);
    }
	}
	
	public void SetPanelActive(bool active)
  {
    gameObject.SetActive(active);
  }

  public void ToggleInfoPanel()
  {
    SetPanelActive(!gameObject.activeSelf);
  }
}
