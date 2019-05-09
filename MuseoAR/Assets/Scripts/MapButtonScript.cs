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
    private List<GameObject> marks = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        AddMarks();
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
            ActivateMarks();
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
            thirdFloorPanel.transform.localScale = Vector3.one * .6f;
        }
        else
        {
            thirdFloorPanel.transform.localScale = Vector3.zero;
        }

    }

    #region adding mark gameobjects
    private void AddMarks()
    {
        GameObject tldr0;
        tldr0 = GameObject.Find("MapPanel/Image/tldr0");
        tldr0.SetActive(false);
        marks.Add(tldr0);

        GameObject tldr1;
        tldr1 = GameObject.Find("MapPanel/Image/tldr1");
        tldr1.SetActive(false);
        marks.Add(tldr1);

        GameObject tldr2;
        tldr2 = GameObject.Find("MapPanel/Image/tldr2");
        tldr2.SetActive(false);
        marks.Add(tldr2);

        GameObject tldr3;
        tldr3 = GameObject.Find("MapPanel/Image/tldr3");
        tldr3.SetActive(false);
        marks.Add(tldr3);

        GameObject tldr4;
        tldr4 = GameObject.Find("MapPanel/Image/tldr4");
        tldr4.SetActive(false);
        marks.Add(tldr4);

        GameObject tldr5;
        tldr5 = GameObject.Find("MapPanel/Image/tldr5");
        tldr5.SetActive(false);
        marks.Add(tldr5);

        GameObject tldr6;
        tldr6 = GameObject.Find("MapPanel/Image/tldr6");
        tldr6.SetActive(false);
        marks.Add(tldr6);

        GameObject tldr7;
        tldr7 = GameObject.Find("MapPanel/Image/tldr7");
        tldr7.SetActive(false);
        marks.Add(tldr7);

        GameObject aarre0;
        aarre0 = GameObject.Find("MapPanel/Image/aarre0");
        aarre0.SetActive(false);
        marks.Add(aarre0);

        GameObject aarre1;
        aarre1 = GameObject.Find("MapPanel/Image/aarre1");
        aarre1.SetActive(false);
        marks.Add(aarre1);

        GameObject aarre2;
        aarre2 = GameObject.Find("MapPanel/Image/aarre2");
        aarre2.SetActive(false);
        marks.Add(aarre2);

        GameObject aarre3;
        aarre3 = GameObject.Find("MapPanel/Image/aarre3");
        aarre3.SetActive(false);
        marks.Add(aarre3);

        GameObject aarre4;
        aarre4 = GameObject.Find("MapPanel/Image/aarre4");
        aarre4.SetActive(false);
        marks.Add(aarre4);

        GameObject aarre5;
        aarre5 = GameObject.Find("MapPanel/Image/aarre5");
        aarre5.SetActive(false);
        marks.Add(aarre5);

        GameObject aarre6;
        aarre6 = GameObject.Find("MapPanel/Image/aarre6");
        aarre6.SetActive(false);
        marks.Add(aarre6);

        GameObject quizScene;
        quizScene = GameObject.Find("MapPanel/Image/quizScene");
        quizScene.SetActive(false);
        marks.Add(quizScene);

        GameObject quizScene2;
        quizScene2 = GameObject.Find("MapPanel/Image/quizScene2");
        quizScene2.SetActive(false);
        marks.Add(quizScene2);

        GameObject quizScene3;
        quizScene3 = GameObject.Find("MapPanel/Image/quizScene3");
        quizScene3.SetActive(false);
        marks.Add(quizScene3);

        GameObject invaders;
        invaders = GameObject.Find("MapPanel/Image/invaders");
        invaders.SetActive(false);
        marks.Add(invaders);

        GameObject Pesapallo;
        Pesapallo = GameObject.Find("MapPanel/Image/Pesapallo");
        Pesapallo.SetActive(false);
        marks.Add(Pesapallo);

        GameObject VideoScene;
        VideoScene = GameObject.Find("MapPanel/Image/360VideoScene");
        VideoScene.name = "360VideoScene";
        VideoScene.SetActive(false);
        marks.Add(VideoScene);

        GameObject PictureScene1;
        PictureScene1 = GameObject.Find("MapPanel/Image/360PictureScene1");
        PictureScene1.name = "360PictureScene1";
        PictureScene1.SetActive(false);
        marks.Add(PictureScene1);

        GameObject PictureScene2;
        PictureScene2 = GameObject.Find("MapPanel/Image/360PictureScene2");
        PictureScene2.name = "360PictureScene2";
        PictureScene2.SetActive(false);
        marks.Add(PictureScene2);
    }
    #endregion

    #region activatemarks
    private void ActivateMarks()
    {
        List<string> tldrScenes = GameControllerScript.tldrScenes;
        List<string> aarreScenes = GameControllerScript.aarreScenes;
        List<string> otherScenes = GameControllerScript.otherScenes;

        foreach (var tldr in tldrScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;
                //tag = tag.Substring(tag.Length - 1);
                string name = "tldr" + tldr;

                if (name == tag)
                {
                    mark.SetActive(true);
                }
            }
        }

        foreach (var aarre in aarreScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;

                string name = "aarre" + aarre;

                if (name == tag)
                {
                    mark.SetActive(true);
                }
            }
        }

        foreach (var other in otherScenes)
        {
            foreach (var mark in marks)
            {
                string tag = mark.name;

                if (other == tag)
                {
                    mark.SetActive(true);
                }
            }
        }
    }
    #endregion
}
