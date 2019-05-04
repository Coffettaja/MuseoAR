using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AarteenetsintaScript : MonoBehaviour
{

    private GameObject infoText;
    private string identifier = GameControllerScript.aarreIdentifier;

    /// <summary>
    /// Finds and displays the text related to the treasure from file.
    /// </summary>
    void Start()
    {
        infoText = GameObject.Find("InfoText");

        int i = 0;
        int.TryParse(identifier, out i);

        string valittu = SimpleJsonScript.Instance.getEntry(i, "aarreBank");

        var texbox = infoText.GetComponent<Text>();
        texbox.text = valittu;

        // Adding the treasure to the list of found treasures.
        GameControllerScript.Instance.LisaaAarre(i);

        string item = "Tophat";

        // Nyt tulee rumaa tavaraa mutta saa kelvata kun koitan pitää muutokset vain yhdessä tiedostossa ja yhdessä kohdassa.
        switch (i)
        {
            case 0:
                item = "Tophat";
                break;
            case 1:
                item = "HipsterGlasses";
                break;
            case 2:
                item = "Moustache";
                break;
            case 3:
                item = "Mohawk";
                break;
            case 4:
                item = "MohawkGreen";
                break;
            case 5:
                item = "Pin";
                break;
            case 6:
                item = "Shovel";
                break;
            default:
                item = "Tophat";
                break;
        }
        // Activating the treasure in selfie scene.
        GameControllerScript.Instance.ActivateDecorations(item);
    }
}