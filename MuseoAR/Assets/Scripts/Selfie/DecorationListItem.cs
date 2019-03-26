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

  Color m_originalColor;
  Image m_decorationImage;

  // Use this for initialization
  void Start()
  {
    m_inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
    m_button = GetComponent<Button>();
    m_decorationImage = GetComponent<Image>();
    m_decorationSprite = m_decorationImage.sprite;

    m_originalColor = m_decorationImage.color;
    Color deactiveColor;
    ColorUtility.TryParseHtmlString("#0000007D", out deactiveColor);

    if (!m_isActive)
    {
      m_decorationImage.color = deactiveColor;
    }
  }

  // What to do when the decoration item is pressed / clicked
  void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
  {
    if (m_isActive)
    {
      GameObject newDecoration = Instantiate(DecorationPrefab) as GameObject;
      newDecoration.name = gameObject.name + "OnScreen";
      newDecoration.GetComponent<SpriteRenderer>().sprite = m_decorationSprite;
      newDecoration.AddComponent<BoxCollider>(); // Has to be added in code so it automatically fits the sprite
      m_inputManager.DraggedObject = newDecoration;
      ScoreScript.Instance.IncreaseScoreBy(1);
    }
  }

  public void Activate()
  {
    if (m_decorationImage != null)
    {
      m_decorationImage.color = m_originalColor;
    }
    m_isActive = true;
  }

  bool DecorationActive()
  {
    return true;//GameControllerScript.Instance.IsSceneCompleted(m_activatingScene);
  }

}
