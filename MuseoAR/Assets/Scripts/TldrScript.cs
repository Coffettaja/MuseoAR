using TMPro;
using UnityEngine;

public class TldrScript : MonoBehaviour {

    private GameObject infoText;
    private string identifier = GameControllerScript.tldrIdentifier;

    /// <summary>
    /// Finds and displays the text related to the scanned TL;DR marker from file.
    /// </summary>
    void Start() {
        infoText = GameObject.Find("TextMeshPro Text");

        int i = 0;
        int.TryParse(identifier, out i);

        string valittu = SimpleJsonScript.Instance.getEntry(i, "tldrBank");
        var texbox = infoText.GetComponent<TextMeshProUGUI>();
        texbox.text = valittu;
    }
}
