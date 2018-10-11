using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Debuggaukseen
public class TextScript : MonoBehaviour
{
	public GameObject asd;

	private SceneItemScript s;
	// Use this for initialization
	void Start () {
		if (asd)
		{
			s = asd.GetComponent<SceneItemScript>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Text>().text = s.getCurrent().ToString();
	}
}
