using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// @haeejuut 10.17.2018
/// </summary>
public class CameraScript : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {

        /*
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
    /// After 0.5 seconds the target will be destroyed.
    /// Functionality of hit objects can be implemented in this dummy.
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    IEnumerator LockOnTarget(RaycastHit hit)
    {
        // Checks if camera center stays on object
        Transform previousAimpoint = hit.transform;
        yield return new WaitForSeconds(1);
        Transform currentAimpoint = hit.transform;
        if (previousAimpoint && currentAimpoint && previousAimpoint == currentAimpoint)
        {
            GameObject destroyable = hit.transform.gameObject;
            if (destroyable.tag == "enemy")
            {
                MeshRenderer m_rend = destroyable.GetComponent<MeshRenderer>();
                m_rend.material.color = Color.red;
            }
            //MeshRenderer m_rend = destroyable.GetComponent<MeshRenderer>();
            //m_rend.material.color = Color.red;
            yield return new WaitForSeconds(.5f);
            // null check here feels reduntand, but sometimes the gameobject
            // gets destroyed before we reach this part 
            if(destroyable && destroyable.tag == "enemy")
            {
                Destroy(destroyable);
            }
        }
    }
}
