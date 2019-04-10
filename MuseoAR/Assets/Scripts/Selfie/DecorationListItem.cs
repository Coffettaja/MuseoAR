using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script for a decoration item listed in the decoration tray
/// Handles instantiating a new decoration object on click/touch
/// </summary>
public class DecorationListItem : MonoBehaviour
{

  SelfieInputManager m_inputManager;
  Button m_button;
  //public GameObject DecorationPrefab;


  private RectTransform m_decoration;

  private Sprite m_decorationSprite;

  public string m_activatingScene;

  private bool m_isActive = false;

  //Color m_originalColor;
  Image m_decorationImage;
  Image m_decorationSlotImage;

  // Use this for initialization
  void Start()
  {
    m_inputManager = GameObject.Find("ARCamera").GetComponent<SelfieInputManager>();
    m_button = GetComponent<Button>();
    m_decorationSlotImage = GetComponent<Image>();
    m_decorationSprite = m_decorationSlotImage.sprite;
    RectTransform rectTransform = transform as RectTransform;

    m_decoration = transform.GetChild(0) as RectTransform;

    m_decoration.sizeDelta = rectTransform.sizeDelta;

    m_decoration.GetComponent<Image>().sprite = m_decorationSprite;
    //m_decoration.SetActive(false);
    //m_decoration.transform.parent = gameObject.transform;

    //m_originalColor = m_decorationImage.color;
    Color deactiveColor;
    ColorUtility.TryParseHtmlString("#0000007D", out deactiveColor);
    m_decorationSlotImage.color = deactiveColor;

    //m_decorationSlotImage

    m_decoration.gameObject.SetActive(m_isActive);
  }

  private void ActivateDecoration()
  {
    //DecorationPrefab.SetActive(true);
  }

  public void IsActive(bool activationState)
  {
    if (m_decorationImage != null)
    {
      //m_decorationImage.color = m_originalColor;
    }

    m_isActive = activationState;
  }

  public bool DecorationActive()
  {
    return m_isActive;
  }

}
