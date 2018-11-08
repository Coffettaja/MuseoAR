using System.Collections;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class SphereScript : MonoBehaviour {

    public TrackableBehaviour tb;
    public float timeToDissolve, dissolveAmount;

    private MeshRenderer rend;
    private GameObject videoSphere, dissolvingSphere;
    private Camera cam;

	// Use this for initialization
	void Start () {
        rend = transform.GetChild(1).GetComponent<MeshRenderer>();
        videoSphere = transform.GetChild(0).gameObject;
        dissolvingSphere = transform.GetChild(1).gameObject;
        cam = FindObjectOfType<Camera>();
        // aloitetaan dissolve kun objekti aktiivinen
        StartCoroutine(startDissolve());
        videoSphere.GetComponent<VideoPlayer>().Prepare();
    }
    
    private void Update()
    {
        if (tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
        {
            dissolvingSphere.SetActive(true);            
            videoSphere.SetActive(true);
        }
        if (videoSphere.activeSelf == true)
        {
            Quaternion q = cam.transform.rotation;
            transform.rotation = Quaternion.Euler(q.x, q.y, q.z);
        }
    }

    /// <summary>
    /// Dissolves the material of the meshrenderer 
    /// </summary>
    /// <returns></returns>
    public IEnumerator startDissolve()
    {        
        if (rend.material.GetFloat("_DissolvePercentage") >= 1)
            yield break;
        
        var dispercentage = rend.material.GetFloat("_DissolvePercentage");
        rend.material.SetFloat("_DissolvePercentage", dispercentage + dissolveAmount);
        yield return new WaitForSecondsRealtime(timeToDissolve);
        StartCoroutine(startDissolve());
    }
    
    /// <summary>
    /// Restart the dissolve effect to test values (dissolveAmount, timeToDissolve)
    /// </summary>
    public void restartDissolve()
    {
        rend.material.SetFloat("_DissolvePercentage", 0);
        StartCoroutine(startDissolve());
    }

    /// <summary>
    /// Exit from the spheres
    /// </summary>
    public void exitTracking()
    {
        dissolvingSphere.SetActive(false);
        videoSphere.SetActive(false);
        videoSphere.GetComponent<VideoPlayer>().Prepare();
        rend.material.SetFloat("_DissolvePercentage", 0);
    }
}
