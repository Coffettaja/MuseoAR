using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Animations for the targeting crosshair on the upper manager AR-view.
/// Calls the LoadScene of TrackableScript after playing animations. In an ideal world this would probably be handled by something else
/// </summary>
public class TargetScript : MonoBehaviour {

    Image targetFrame;
    Image targetFill;

    Vector3 fillScale;
    IEnumerator c;
    IEnumerator c2;

    TrackableScript targetedMarker;

	// Use this for initialization
	void Start () {
        targetFrame = GetComponent<Image>();
        targetFill = transform.Find("TargetFill").GetComponent<Image>();        
        fillScale = targetFill.transform.localScale;

        //targetFill.CrossFadeAlpha(1, 2.0f, false);
        c = FillCoroutine(2.0f);
        c2 = AfterPulse(0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetFill.rectTransform.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit) )
        {
            if(hit.collider.gameObject.tag == "marker")
            {
                targetedMarker = hit.collider.gameObject.GetComponent<TrackableScript>();
                StartCoroutine(c);
            }
        }
        else
        {
            targetedMarker = null;
        }
	}

    //Fill and fade in the targeting diamond
    private IEnumerator FillCoroutine(float fillTime)
    {
        float startTime = Time.time;
        float endTime = startTime + fillTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / (endTime - startTime);
            targetFill.transform.localScale = fillScale * t;
            Color col = targetFill.color;
            targetFill.color = new Color(col.r, col.b, col.g, 0.75f * t);
            yield return null;
        }
        StartCoroutine(c2);
    }

    //
    private IEnumerator AfterPulse(float fillTime)
    {
        float startTime = Time.time;
        float endTime = startTime + fillTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / (endTime - startTime);
            targetFill.transform.localScale = fillScale * (1f+t);
            Color col = targetFill.color;
            targetFill.color = new Color(col.r, col.b, col.g, 0.50f * (1-t));
            yield return null;
        }
        targetedMarker.LoadScene();
    }
}
