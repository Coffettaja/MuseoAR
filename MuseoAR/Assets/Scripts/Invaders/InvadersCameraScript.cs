using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// @haeejuut 10.17.2018
/// </summary>
public class InvadersCameraScript : MonoBehaviour {

    private void Start()
    {
        VuforiaARController.Instance.SetWorldCenterMode(VuforiaARController.WorldCenterMode.FIRST_TARGET);
    }
    // Update is called once per frame
    void Update () {

        /*
         * Sends a ray from camera (script as camera component) center.
         * Starts Coroutine on hit.
        */
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

        /* Commented out for testing collider based shooting
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            StartCoroutine(LockOnTarget(hit));
        }*/
	}

    /// <summary>
    /// If you hit the target for 1 second, it will change color to red.
    /// Then it will tell the object to die.
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
            GameObject hitTarget = hit.transform.gameObject;
            hitTarget.GetComponent<EnemyScript>().Die();
        }
    }
}
