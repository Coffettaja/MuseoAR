using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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

  private bool m_isActive = false;

  private GameObject m_canvas;

  private AspectRatioFitter m_aspectRatioFitter;

  Image m_decorationImage;
  Image m_decorationSlotImage;

  GameObject dummyItem; // Used for correctly layering the elements on drag.

  // Use this for initialization
  void Start()
  {
    dummyItem = GameObject.FindGameObjectWithTag("DummyItem");
    m_inputManager = Camera.main.GetComponent<SelfieInputManager>();
    m_button = GetComponent<Button>();
    m_button.onClick.AddListener(OnDecorationButtonClick);
    m_decorationSlotImage = GetComponent<Image>();
    m_decorationSprite = m_decorationSlotImage.sprite;
    RectTransform rectTransform = transform as RectTransform;
    m_canvas = GameObject.FindWithTag("Canvas");

    m_decoration = transform.GetChild(0) as RectTransform;

    m_aspectRatioFitter = m_decoration.GetComponent<AspectRatioFitter>();

    m_decoration.sizeDelta = rectTransform.sizeDelta;

    m_decoration.GetComponent<Image>().sprite = m_decorationSprite;

    Color deactiveColor;
    ColorUtility.TryParseHtmlString("#0000007D", out deactiveColor);
    m_decorationSlotImage.color = deactiveColor;

    //m_decorationSlotImage

    m_decoration.gameObject.SetActive(m_isActive);
  }

  private void OnDecorationButtonClick()
  {
    if (transform.childCount > 0 && m_isActive)
    {
      var rt = (RectTransform)m_decoration.transform;
      m_decoration.transform.SetParent(m_canvas.transform);
      m_decoration.transform.SetSiblingIndex(dummyItem.transform.GetSiblingIndex());
      rt.anchorMin = Vector2.one / 2;
      rt.anchorMax = Vector2.one / 2;
      rt.pivot = Vector2.one / 2;
      rt.anchoredPosition = Vector2.zero;
      if (m_aspectRatioFitter != null)
      {
        m_aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.None;
      }
    }
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

    if (activationState)
    {
      transform.SetAsFirstSibling();
    }

    m_isActive = activationState;
  }

  public bool DecorationActive()
  {
    return m_isActive;
  }

}
