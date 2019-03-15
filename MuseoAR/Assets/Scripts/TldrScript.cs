using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TldrScript : MonoBehaviour {

    private List<Tldr> tldrList;
    private GameObject infoText;
    public string identifier = GameControllerScript.tldrIdentifier;

    void Start() {
        infoText = GameObject.Find("InfoText");
        tldrList = new List<Tldr>();
        fromJsonToList();
        getInfo();
        Debug.Log(identifier);
    }
	
    private void getInfo()
    {
        int i = 0;
        int.TryParse(identifier, out i);
        Tldr valittu = tldrList[i];
        var texbox = infoText.GetComponent<Text>();
        texbox.text = valittu.tldr;
    }


    private void fromJsonToList()
    {
        Debug.Log(Application.dataPath);
        TextAsset ladattava = Resources.Load<TextAsset>("tldrBank");
        rootTldr root = JsonUtility.FromJson<rootTldr>(ladattava.text);

        foreach (var q in root.tldrs)
        {
            tldrList.Add(q);
        }
    }


    [Serializable]
    public class Tldr
    {
        public string tldr;

        public override string ToString()
        {
            return string.Format("Info: {0}",
                                 tldr);
        }
    }

    [Serializable]
    public class rootTldr
    {
        public Tldr[] tldrs;
    }
}
