using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {

        /* @haeejuut
         * Sends a ray from camera (script as camera component) center.
         * Starts Coroutine on hit.
        */
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            StartCoroutine(LockOnTarget(hit));
        }
	}

    /// <summary>
    /// If you hit the target for 1 second, it will change color to red.
    /// Functionality of hit objects can be implemented in this dummy.
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
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
