using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Callback scripts for button to hide and show the map model
/// </summary>
public class RoomButtonScript : MonoBehaviour
{
	private GameObject _room;

	private Text _buttonText;

	private Vector3 startPos;
	private Vector3 targetPos;

	private Button _button;
	
	// Use this for initialization
	void Start ()
	{
		_room = GameObject.FindGameObjectWithTag("Map");

		startPos = _room.transform.localPosition + (Vector3.left * 100);
		targetPos = _room.transform.localPosition;
		_room.transform.localPosition = startPos;
		_button = GetComponent<Button>();

		_buttonText = GetComponentInChildren<Text>();
		
		_button.onClick.AddListener(ShowRoom);
	}


	void ShowRoom()
	{
		Debug.Log("Moving map to target position " + targetPos);
		_room.transform.localPosition = targetPos;
		_room.GetComponent<MapScript>().UpdateItems();

		_button.onClick.RemoveListener(ShowRoom);
		_button.onClick.AddListener(HideRoom);
	}

	void HideRoom()
	{
		Debug.Log("Moving map to start position " + startPos);
		_room.transform.localPosition = startPos;

		_button.onClick.RemoveListener(HideRoom);
		_button.onClick.AddListener(ShowRoom);
	}
}
