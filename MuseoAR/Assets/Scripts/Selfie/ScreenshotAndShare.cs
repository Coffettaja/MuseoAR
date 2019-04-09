using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the camera button. Attaches onClick listener that takes a screenshot of the screen
/// and triggers the native share function
/// </summary>
public class ScreenshotAndShare : MonoBehaviour {

  public List<RectTransform> itemsToHide;
  private Vector3 initScale;

  // Use this for initialization
  void Start () {
    Button screenshotButton = GetComponent<Button>();

    screenshotButton.onClick.AddListener(StartScreenshotCoroutine);
	}
	
  private void HideCanvas()
  {
    foreach (RectTransform item in itemsToHide)
    {
      item.localScale = Vector3.zero;
    }
  }

  private void StartScreenshotCoroutine()
    {
    HideCanvas();

    StartCoroutine("TakeAndShare");
  }

  private void ShowCanvas()
  {
    foreach (RectTransform item in itemsToHide)
    {
      item.localScale = Vector3.one;
    }
  }

  private IEnumerator TakeAndShare()
    {
        yield return new WaitForEndOfFrame();

        //Direct example from https://github.com/yasirkula/UnityNativeShare on how to share screenshot with API, should work well with other approaches later on too
        
        //Texture2D ss = new Texture2D(Screen.width, (int)screenHeight, TextureFormat.RGB24, false);
        //TODO: Hide the UI while takign screenshot
        Texture2D ss = ScreenCapture.CaptureScreenshotAsTexture();
    ShowCanvas();
        //ss.ReadPixels(new Rect(0, startHeight, Screen.width, screenHeight), 0, 0);
        //ss.Apply();

    string name = string.Format("{0}_{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName, name));
    string filePath = Path.Combine(Application.temporaryCachePath, name + ".png");
      File.WriteAllBytes(filePath, ss.EncodeToPNG());

    // To avoid memory leaks
    GameObject.Destroy(ss);

        NativeShare nativeShare = new NativeShare();
    
        nativeShare.SetSubject("MuseoAR Selfie!"); // Primarily for email.
        //nativeShare.SetText("");
        nativeShare.AddFile(filePath, "image/png");
        nativeShare.SetTitle("Score in Sunset Falls");
        nativeShare.Share();

    }
}
