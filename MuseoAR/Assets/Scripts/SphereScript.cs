using System.Collections;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class SphereScript : MonoBehaviour {

    public TrackableBehaviour tb;
    public float timeToDissolve, dissolveAmount;

    private MeshRenderer rend;
    private GameObject videoSphere, dissolvingSphere, GameController;
    private Camera cam;

	// Use this for initialization
	void Awake () {
        rend = transform.GetChild(1).GetComponent<MeshRenderer>();
        videoSphere = transform.GetChild(0).gameObject;
        dissolvingSphere = transform.GetChild(1).gameObject;
        cam = Camera.main;
        // aloitetaan dissolve kun objekti aktiivinen
        StartCoroutine(startDissolve());
        videoSphere.GetComponent<VideoPlayer>().Prepare();
    }
    
    /// <summary>
    /// Sets video- and dissolvespheres active when imageTarget is found.
    /// Uses rotation of device when spheres are active.
    /// </summary>
    private void Update()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
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
    /// Dissolves the material of the meshrenderer, uses a noice texture for the pattern.
    /// Use dissolveAmount and timeToDissolve to change the dissolve-effect.
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
    /// Restarts the dissolve effect to test values (dissolveAmount, timeToDissolve).
    /// </summary>
    public void restartDissolve()
    {
        rend.material.SetFloat("_DissolvePercentage", 0);
        StartCoroutine(startDissolve());
    }

    ///// <summary>
    ///// Exits from the spheres into "regular" camera mode.
    ///// </summary>
    //public void exitTracking()
    //{
    //    dissolvingSphere.SetActive(false);
    //    videoSphere.SetActive(false);
    //    // Video must be prepared again for it work neater on sphere reload
    //    videoSphere.GetComponent<VideoPlayer>().Prepare();
    //    rend.material.SetFloat("_DissolvePercentage", 0);
    //    GameCTRL.MarkSceneCompleted("360VideoScene");
    //}

    public void exitScene()
    {
        GameControllerScript.Instance.LoadTopLevelScene();
    }
}
