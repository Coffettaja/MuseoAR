using UnityEngine;
using UnityEngine.UI;

public class ToInit : MonoBehaviour
{
    private GameObject InputName;
    private GameObject BackToMenuButton;

    // Use this for initialization
    void Start()
    {
        BackToMenuButton = GameObject.Find("BackToMenuButton");
        Button button = BackToMenuButton.GetComponent<Button>();
        button.onClick.AddListener(ReturnToMainSceneOnClick);
    }

    /// <summary>
    /// Gets one random profession from a file and adds it to the username. Then transitions to Init scene.
    /// </summary>
    void ReturnToMainSceneOnClick()
    {
        string ammatti = SimpleJsonScript.Instance.getEntry(-1, "ammattiBank");

        InputName = GameObject.Find("InputName");
        var inputField = InputName.GetComponent<InputField>();
        string nimi = inputField.text;

        GameControllerScript.nimiJaAmmatti = ammatti + " " + nimi;
        GameControllerScript.Instance.LoadTopLevelScene();
    }

}
