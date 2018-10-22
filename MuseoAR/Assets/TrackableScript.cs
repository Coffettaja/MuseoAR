using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableScript : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour _trackableBehaviour;
    private GameControllerScript _gameController;
    public string sceneName;
    
	// Use this for initialization
	void Start () {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        if(_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        _gameController = GameObject.Find("gameController").GetComponent<GameControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.TRACKED)
        {
            
        }
        
    }

    public void LoadScene()
    {
        _gameController.LoadSceneWithName(sceneName);
    }

    public TrackableBehaviour.Status getCurrent()
    {
        return _trackableBehaviour.CurrentStatus;
    }
}
