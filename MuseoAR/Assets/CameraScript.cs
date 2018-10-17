using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            StartCoroutine(LockOnTarget(hit));
        }
	}

    IEnumerator LockOnTarget(RaycastHit hit)
    {
        Transform wanha = hit.transform;
        yield return new WaitForSeconds(1);
        Transform uusi = hit.transform;
        if (wanha == uusi)
        {
            MeshRenderer m_rend = hit.transform.GetComponent<MeshRenderer>();
            m_rend.material.color = Color.red;
        }
    }
}
