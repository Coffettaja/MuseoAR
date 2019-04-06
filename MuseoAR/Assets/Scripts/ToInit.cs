using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToInit : MonoBehaviour
{
    private GameObject InputName;
    private GameObject BackToMenuButton;
    private List<Ammatti> ammattiList;


    // Use this for initialization
    void Start()
    {
        BackToMenuButton = GameObject.Find("BackToMenuButton");
        Button button = BackToMenuButton.GetComponent<Button>();
        button.onClick.AddListener(ReturnToMainSceneOnClick);
    }

    void ReturnToMainSceneOnClick()
    {
        ammattiList = new List<Ammatti>();
        fromJsonToList();

        System.Random rnd = new System.Random();
        int i = rnd.Next(0, ammattiList.Count);

        Ammatti valittu = ammattiList[i];
        string ammatti = valittu.ammatti;

        InputName = GameObject.Find("InputName");
        var inputField = InputName.GetComponent<InputField>();
        string nimi = inputField.text;

        GameControllerScript.nimiJaAmmatti = ammatti + " " + nimi;
        GameControllerScript.Instance.LoadTopLevelScene();
    }

    private void fromJsonToList()
    {
        TextAsset ladattava = Resources.Load<TextAsset>("ammattiBank");
        rootAmmatti root = JsonUtility.FromJson<rootAmmatti>(ladattava.text);

        foreach (var q in root.ammatit)
        {
            ammattiList.Add(q);
        }
    }


    [Serializable]
    public class Ammatti
    {
        public string ammatti;

        public override string ToString()
        {
            return string.Format("Info: {0}",
                                 ammatti);
        }
    }

    [Serializable]
    public class rootAmmatti
    {
        public Ammatti[] ammatit;
    }


}
