using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script for a decoration item listed in the decoration tray
/// Handles instantiating a new decoration object on click/touch
/// </summary>
public class DecorationListItem : MonoBehaviour, IPointerDownHandler {

    SelfieInputManager m_inputManager;
    Button m_button;
    Object m_decorationPrefab;
    Sprite m_decorationSprite;

    // Use this for initialization
	void Start () {
        m_inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
        m_button = GetComponent<Button>();
        //m_button.onClick.AddListener(InstantiateNewDecoration);
        m_decorationSprite = GetComponent<Image>().sprite;
        m_decorationPrefab = Resources.Load("Prefabs/Decoration");
	}

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Kissa");
        GameObject newDecoration = Instantiate(m_decorationPrefab) as GameObject;
        newDecoration.GetComponent<SpriteRenderer>().sprite = m_decorationSprite;
        //newDecoration.transform.position = eventData.position;
        m_inputManager.DraggedObject = newDecoration;
    }
	
}
