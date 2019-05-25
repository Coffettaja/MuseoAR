using UnityEngine;

public class UpdateName : MonoBehaviour {

    private GameObject PlayerName;

	// Use this for initialization
	void Start () {
        string nameAndProfession = GameControllerScript.nimiJaAmmatti;

        PlayerName = GameObject.Find("PlayerName");
        var textMesh = PlayerName.GetComponent<TextMesh>();
        textMesh.text = nameAndProfession;

    }
}
