using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager gameManagerInstance;

	private bool _sceneVisited = false;

	public bool SceneVisited
	{
		get { return _sceneVisited; }
		set { _sceneVisited = value; }
	}

	// Use this for initialization
	void Awake()
	{
		if (gameManagerInstance == null)
		{
			gameManagerInstance = this;
		} else if (gameManagerInstance != this)
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
