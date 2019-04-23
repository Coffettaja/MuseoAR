using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the camera button. Attaches onClick listener that takes a screenshot of the screen
/// and triggers the native share function
/// </summary>
public class ScreenshotAndShare : MonoBehaviour {

  public List<RectTransform> itemsToHide;
  public bool publicDevice = false;
  public RectTransform canvasRectTransform;
  //private Vector3 initScale;

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

    StartCoroutine("TakeScreenshot");
  }

  private void ShowCanvas()
  {
    foreach (RectTransform item in itemsToHide)
    {
      item.localScale = Vector3.one;
    }
  }

  private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

    //Direct example from https://github.com/yasirkula/UnityNativeShare on how to share screenshot with API, should work well with other approaches later on too

    //Texture2D ss = new Texture2D(Screen.width, (int)screenHeight, TextureFormat.RGB24, false);
    //TODO: Hide the UI while takign screenshot
    Texture2D ss = ScreenCapture.CaptureScreenshotAsTexture(1); // larger values crash unity...
    
    // Alternative method - which one is better?
    //Texture2D ss = new Texture2D(Screen.width, Screen.height);
    //ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //ss.Apply();

    ShowCanvas();

    Debug.Log(ss.width + "x" + ss.height);

    string name = string.Format("{0}_{1}", Application.productName, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    //Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName, name));
    string filePath = Path.Combine(Application.temporaryCachePath, name + ".png");
    File.WriteAllBytes(filePath, ss.EncodeToPNG());

    /*
    var canvas = GameObject.FindWithTag("Canvas");
    var imageResultGO = new GameObject();
    imageResultGO.transform.parent = canvas.transform;
    imageResultGO.name = "Can there be a name for this one????";
    var imageResult = imageResultGO.AddComponent<Image>();
    var rectangle = canvasRectTransform.rect;
    imageResult.sprite = Sprite.Create(ss, rectangle, new Vector2(.5f, .5f));

    ss.name = "SUPER NAME HERE HAHAHA";
    */
    // To avoid memory leaks
    Destroy(ss);
    //yield break;

    if (publicDevice)
    {
      PublicShare(filePath);
    } else
    {
      Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName, name));
      NativeShare nativeShare = new NativeShare();

      nativeShare.SetSubject("MuseoAR Selfie!"); // Primarily for email.
                                                 //nativeShare.SetText("");
      nativeShare.AddFile(filePath, "image/png");
      nativeShare.SetTitle("Score in Sunset Falls");
      nativeShare.Share();
    }
  }

  // More or less copied from
  // https://answers.unity.com/questions/1184875/email-attachment-on-android-ios.html
  private void SendEmail(string destinationEmail, string filepath)
  {
    string FileName = "";

    Debug.Log("In public share");

    if(!File.Exists(filepath)) {
      Debug.Log(filepath + " doesn't exist.");
      return;
    }

    MailMessage mail = new MailMessage();

    mail.From = new MailAddress("museoartestemail@gmail.com");
    mail.To.Add(destinationEmail);
    mail.Subject = "MuseoAR Selfie";
    mail.Body = "Kawaii~";

    Attachment data = new Attachment(filepath, System.Net.Mime.MediaTypeNames.Application.Octet);

    // Add time stamp information for the file.
    System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;
    disposition.CreationDate = System.IO.File.GetCreationTime(filepath);
    disposition.ModificationDate = System.IO.File.GetLastWriteTime(filepath);
    disposition.ReadDate = System.IO.File.GetLastAccessTime(filepath);

    mail.Attachments.Add(data);

    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
    smtpServer.Port = 587;
    smtpServer.Credentials = new System.Net.NetworkCredential("museoartestmail@gmail.com", "???") as ICredentialsByHost;
    smtpServer.EnableSsl = true;
    ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
          return true;
        };
    try
    {
      smtpServer.Send(mail);
    }
    catch (Exception e)
    {
      Debug.Log(e.GetBaseException());
    }
  }

  
  private void PublicShare(string filepath)
  {
    SendEmail("destinationEmailHere", filepath);
  }
}
