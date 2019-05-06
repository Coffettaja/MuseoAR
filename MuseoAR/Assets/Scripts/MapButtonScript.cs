using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonScript : MonoBehaviour
{

  public GameObject secondFloorButton;
  public GameObject thirdFloorButton;
  public GameObject secondFloorPanel;
  public GameObject thirdFloorPanel;

  private Button m_button;
  private bool isOpen;
  private bool activateSecondFloor = true;
  private MapGestureManager gestureManager;

  // Use this for initialization
  void Start()
  {
    m_button = GetComponent<Button>();
    m_button.onClick.AddListener(ToggleMap);
    secondFloorButton.GetComponent<Button>().onClick.AddListener(ShowSecondFloor);
    thirdFloorButton.GetComponent<Button>().onClick.AddListener(ShowThirdFloor);
    SetSecondFloorActive(false);
    SetThirdFloorActive(false);
    secondFloorButton.SetActive(false);
    thirdFloorButton.SetActive(false);
    gestureManager = GameObject.FindWithTag("Canvas").GetComponent<MapGestureManager>();
    isOpen = false;
  }

  private void ToggleMap()
  {
    // Activate the first floor map and the button of the second floor map.
    if (!isOpen)
    {
      secondFloorButton.SetActive(true);
      thirdFloorButton.SetActive(true);
      if (activateSecondFloor)
      {
        ShowSecondFloor();
      }
      else // Third floor
      {
        ShowThirdFloor();
      }


      isOpen = true;
    }
    // Deactivate all maps and floor buttons.
    else
    {
      SetSecondFloorActive(false);
      SetThirdFloorActive(false);
      secondFloorButton.SetActive(false);
      thirdFloorButton.SetActive(false);
      isOpen = false;
    }
  }

  public void ShowSecondFloor()
  {
    SetSecondFloorActive(true);
    SetThirdFloorActive(false);
    secondFloorButton.GetComponent<Button>().interactable = false;
    thirdFloorButton.GetComponent<Button>().interactable = true;
    gestureManager.SetMap(secondFloorPanel);
    activateSecondFloor = true;
  }

  public void ShowThirdFloor()
  {
    SetSecondFloorActive(false);
    SetThirdFloorActive(true);
    secondFloorButton.GetComponent<Button>().interactable = true;
    thirdFloorButton.GetComponent<Button>().interactable = false;
    gestureManager.SetMap(thirdFloorPanel);
    activateSecondFloor = false;
  }

  private void SetSecondFloorActive(bool active)
  {
    if (active)
    {
      secondFloorPanel.transform.localScale = Vector3.one * .6f;
    } else
    {
      secondFloorPanel.transform.localScale = Vector3.zero;
    }
  }

  private void SetThirdFloorActive(bool active)
  {
    if (active)
    {
      thirdFloorPanel.transform.localScale = Vector3.one * .6f;
    } else
    {
      thirdFloorPanel.transform.localScale = Vector3.zero;
    }

  }
}
