using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script for a decoration item listed in the decoration tray
/// Handles instantiating a new decoration object on click/touch
/// </summary>
public class DecorationListItem : MonoBehaviour, IPointerDownHandler
{

  SelfieInputManager m_inputManager;
  Button m_button;
  public GameObject DecorationPrefab;

  private Sprite m_decorationSprite;

  public string m_activatingScene;

  private bool m_isActive = false;

  // Use this for initialization
  void Start()
  {
    m_inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
    m_button = GetComponent<Button>();
    var decorationImage = GetComponent<Image>();
    m_decorationSprite = decorationImage.sprite;

    Color deactiveColor;
    ColorUtility.TryParseHtmlString("#0000007D", out deactiveColor);

    if (!DecorationActive())
    {
      decorationImage.color = deactiveColor;
    }
  }

  // What to do when the decoration item is pressed / clicked
  void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
  {
    if (DecorationActive())
    {
      GameObject newDecoration = Instantiate(DecorationPrefab) as GameObject;
      newDecoration.GetComponent<SpriteRenderer>().sprite = m_decorationSprite;
      m_inputManager.DraggedObject = newDecoration;
    }
  }

  public void Activate()
  {
    m_isActive = true;
  }

  bool DecorationActive()
  {
    return true;//GameControllerScript.Instance.IsSceneCompleted(m_activatingScene);
  }

}
