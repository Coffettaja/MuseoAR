using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationTrayScript : MonoBehaviour {

  public Button decorationButtonPrefab;

  private RectTransform decorationTrayTransform;

	// Use this for initialization
	void Start () {
    CreateDecorationButtons();
	}

  private void CreateDecorationButtons()
  {
    List<Button> list = new List<Button>();

    decorationTrayTransform = gameObject.GetComponent<RectTransform>();
    var trayRectTransform = gameObject.GetComponent<RectTransform>();
    var scrollRectTransform = gameObject.transform.parent.GetComponent<RectTransform>();
    float buttonWidth = scrollRectTransform.sizeDelta.x * 0.75f;
    var buttonDimensions = new Vector2(buttonWidth, buttonWidth);
    float buttonBottomMargin = 20f;
    float buttonSpace = buttonWidth + buttonBottomMargin;

    for (int i = 0; i < 16; i++)
    {
      Button decorationButton = Instantiate(decorationButtonPrefab);
      decorationButton.transform.SetParent(gameObject.transform, false);
      var buttonRectTransform = decorationButton.GetComponent<RectTransform>();
      buttonRectTransform.sizeDelta = buttonDimensions;
      buttonRectTransform.anchoredPosition = new Vector2(0, 16 * buttonSpace / 2 - i * buttonSpace);
      trayRectTransform.sizeDelta = new Vector2(trayRectTransform.sizeDelta.x, trayRectTransform.sizeDelta.y + buttonWidth + buttonBottomMargin + 30f);
    }
  }
}
