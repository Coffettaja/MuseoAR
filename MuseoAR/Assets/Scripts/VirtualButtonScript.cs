using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VirtualButtonScript : MonoBehaviour {

    private GameObject tekstiboksi;

    // Use this for initialization
    void Start()
    {
        tekstiboksi = GameObject.Find("testiText");
    }
}
