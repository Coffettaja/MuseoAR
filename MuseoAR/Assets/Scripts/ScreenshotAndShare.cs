using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotAndShare : MonoBehaviour {

    private Button m_screenshotButton;

	// Use this for initialization
	void Start () {
        m_screenshotButton = GetComponent<Button>();
        m_screenshotButton.onClick.AddListener(StartScreenshotCoroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StartScreenshotCoroutine()
    {
        StartCoroutine("TakeAndShare");
    }

    private IEnumerator TakeAndShare()
    {
        yield return new WaitForEndOfFrame();

        //Direct example from https://github.com/yasirkula/UnityNativeShare on how to share screenshot with API, should work well with other approaches later on too
        
        //Texture2D ss = new Texture2D(Screen.width, (int)screenHeight, TextureFormat.RGB24, false);
        Texture2D ss = ScreenCapture.CaptureScreenshotAsTexture();
        //ss.ReadPixels(new Rect(0, startHeight, Screen.width, screenHeight), 0, 0);
        //ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "sharedScore.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        GameObject.Destroy(ss);

        NativeShare nativeShare = new NativeShare();
        nativeShare.SetSubject("Viiksikuva!");
        nativeShare.SetText("'mlady!");
        nativeShare.AddFile(filePath, "image/png");
        nativeShare.SetTitle("Score in Sunset Falls");
        nativeShare.Share();
    }
}
