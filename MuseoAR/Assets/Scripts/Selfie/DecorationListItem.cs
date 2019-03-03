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
    public GameObject DecorationPrefab;

    public Sprite m_decorationSprite;
    public Sprite m_deactivatedSprite;

    public string m_activatingScene;

    // Use this for initialization
	void Start ()
    {
        m_inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
        m_button = GetComponent<Button>();
        //m_button.onClick.AddListener(InstantiateNewDecoration);
        m_decorationSprite = GetComponent<Image>().sprite;
        if(DecorationActive())
        {
            GetComponent<Image>().sprite = m_decorationSprite;
        } else
        {
            GetComponent<Image>().sprite = m_deactivatedSprite;
        }
	}

  // What to do when the decoration item is pressed / clicked
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(DecorationActive())
        {
            GameObject newDecoration = Instantiate(DecorationPrefab) as GameObject;
            newDecoration.GetComponent<SpriteRenderer>().sprite = m_decorationSprite;
            m_inputManager.DraggedObject = newDecoration;
        }
    }

    bool DecorationActive()
    {
    return true;//GameControllerScript.Instance.IsSceneCompleted(m_activatingScene);
    }
	
}
