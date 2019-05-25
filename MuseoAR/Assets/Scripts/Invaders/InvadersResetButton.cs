using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for handling the reset button after game over.
/// </summary>
public class InvadersResetButton : MonoBehaviour {

    InvadersManagerScript m_manager;
    public GameObject managerParentObject;

    void Start () {
        m_manager = managerParentObject.GetComponent<InvadersManagerScript>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ResetScene);
	}
	
    void ResetScene()
    {
        m_manager.ResetGame();
    }
}
