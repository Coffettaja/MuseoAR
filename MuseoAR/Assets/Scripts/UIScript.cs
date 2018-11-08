using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public string testHeader, testText;

    private GameObject infoPanel;
    private Text infoHeader, infoText;

    // Use this for initialization
    void Start () {
        infoPanel = GameObject.Find("InfoPanel");
        infoHeader = GameObject.Find("TextHeader").GetComponent<Text>();
        infoText = GameObject.Find("TextInfoBox").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
