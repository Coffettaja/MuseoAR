using UnityEngine;
using UnityEngine.UI;

public class MapButtonScript : MonoBehaviour
{

  public GameObject secondFloorButton;
  public GameObject thirdFloorButton;
  public GameObject secondFloorPanel;
  public GameObject thirdFloorPanel;
  public GameObject mapPanel;

  private Button m_button;
  private bool isOpen;
  private bool activateSecondFloor = true;
  private MapGestureManager gestureManager;

  // Use this for initialization
  void Start()
  {
    m_button = GetComponent<Button>();
    Invoke("DelayToggleMap", 1);
    secondFloorButton.GetComponent<Button>().onClick.AddListener(ShowSecondFloor);
    thirdFloorButton.GetComponent<Button>().onClick.AddListener(ShowThirdFloor);

    // Hide everything initially.
    SetSecondFloorActive(false);
    SetThirdFloorActive(false);
    SetMapPanelActive(false);

    // Need this to set the map that will be moved (second or third floor).
    gestureManager = GameObject.FindWithTag("Canvas").GetComponent<MapGestureManager>();
  }

  private void DelayToggleMap()
  {
    m_button.onClick.AddListener(ToggleMap);
  }

  private void ToggleMap()
  {
    SetMapPanelActive(!isOpen);

    if (isOpen)
    {
      if (activateSecondFloor)
      {
        ShowSecondFloor();
      }
      else
      {
        ShowThirdFloor();
      }
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
      // Initially on the scene the map have a scale of 0.6 for some reason so keeping it the same.
      secondFloorPanel.transform.localScale = Vector3.one * .6f;
    }
    else
    {
      secondFloorPanel.transform.localScale = Vector3.zero;
    }
  }

  private void SetThirdFloorActive(bool active)
  {
    if (active)
    {
      // Initially on the scene the map have a scale of 0.6 for some reason so keeping it the same.
      thirdFloorPanel.transform.localScale = Vector3.one * .6f;
    }
    else
    {
      thirdFloorPanel.transform.localScale = Vector3.zero;
    }
  }

  private void SetMapPanelActive(bool active)
  {
    isOpen = active;
    if (active)
    {
      mapPanel.transform.localScale = Vector3.one;
    }
    else
    {
      mapPanel.transform.localScale = Vector3.zero;
    }
  }
}