using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour {


    ManagerScript manager;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        manager = GameObject.Find("ImageTarget").GetComponent<ManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = ""+manager.Score;
	}
}
