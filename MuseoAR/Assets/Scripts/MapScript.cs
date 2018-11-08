using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes care of marking completed scenes on the map
/// </summary>
public class MapScript : MonoBehaviour
{
	private GameObject[] mapItems;
	// Use this for initialization
	void Start ()
	{
		mapItems = GameObject.FindGameObjectsWithTag("MapItem");
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void UpdateItems()
	{
		foreach (GameObject item in mapItems)
		{
			if (GameControllerScript.gameManagerInstance.IsSceneCompleted(item.name))
			{
				item.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			}
		}
	}
}
