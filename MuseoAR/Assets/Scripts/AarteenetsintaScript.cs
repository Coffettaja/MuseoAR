using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AarteenetsintaScript : MonoBehaviour {

    private List<Aarre> aarreList;
    private GameObject infoText;
    private string identifier = GameControllerScript.aarreIdentifier;

    void Start()
    {
        infoText = GameObject.Find("InfoText");
        aarreList = new List<Aarre>();
        fromJsonToList();
        getInfo();      
    }

    public void getInfo()
    {
        int i = 0;
        int.TryParse(identifier, out i);
        Aarre valittu = aarreList[i];
        var texbox = infoText.GetComponent<Text>();
        texbox.text = valittu.aarre;
        GameControllerScript.Instance.LisaaAarre(i);
    }


    private void fromJsonToList()
    {
        //Debug.Log(Application.dataPath);
        TextAsset ladattava = Resources.Load<TextAsset>("aarreBank");
        rootAarre root = JsonUtility.FromJson<rootAarre>(ladattava.text);

        foreach (var q in root.aarteet)
        {
            aarreList.Add(q);
        }
    }


    [Serializable]
    public class Aarre
    {
        public string aarre;

        public override string ToString()
        {
            return string.Format("Info: {0}",
                                 aarre);
        }
    }

    [Serializable]
    public class rootAarre
    {
        public Aarre[] aarteet;
    }
}