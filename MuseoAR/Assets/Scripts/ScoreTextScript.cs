using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour {


    InvadersManagerScript manager;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        manager = GameObject.Find("ImageTarget").GetComponent<InvadersManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = ""+manager.Score;
	}
}
