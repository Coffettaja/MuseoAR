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

        int mark = 0;
        string idString = "";
        string item = "";

        // Dividing the identifier to id and item name.
        try
        {
            mark = identifier.IndexOf("_");
            idString = identifier.Substring(mark + 1);
            item = identifier.Substring(0, mark);
            Debug.Log(idString);
            Debug.Log(item);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            idString = "0";
            item = "HipsterGlasses";
        }
        
        int id = 0;
        int.TryParse(idString, out id);

        // Finding the right celebratory text from the JSON-file.
        string valittu = SimpleJsonScript.Instance.getEntry(id, "aarreBank");

        // Finding the text slot and inserting the right string in it.
        var texbox = infoText.GetComponent<Text>();
        texbox.text = valittu;
   
        // Activating the treasure in selfie scene.
        GameControllerScript.Instance.ActivateDecorations(item);
    }
}