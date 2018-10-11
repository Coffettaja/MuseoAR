using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SceneItemScript : MonoBehaviour, ITrackableEventHandler
{

	private TrackableBehaviour _trackableBehaviour;
	// Use this for initialization
	void Start ()
	{
		_trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (_trackableBehaviour)
		{
			_trackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(_trackableBehaviour.CurrentStatus);
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.TRACKED)
		{
			Debug.Log("Found!");
			GameManager.gameManagerInstance.SceneVisited = true;
		}
	}

	public TrackableBehaviour.Status getCurrent()
	{
		return _trackableBehaviour.CurrentStatus;
	}

}
