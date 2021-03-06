﻿using UnityEngine;
using DigitalRubyShared;

public class MapGestureManager : MonoBehaviour
{

  private float maxSize = 24f;
  private float minSize = 9f;
  //public float rotationSpeed = 2.5f;

  private FingersScript fingerScript;

  private TapGestureRecognizer tapGesture;

  //private SwipeGestureRecognizer swipeGesture;
  private PanGestureRecognizer panGesture;
  //private RotateGestureRecognizer rotateGesture;
  private ScaleGestureRecognizer scaleGesture;
  //private LongPressGestureRecognizer longPressGesture;

  private RectTransform rectTransform;

  private void Awake()
  {
    Debug.Log("AWAKEEE");
  }

  // Use this for initialization
  void Start()
  {
    //rectTransform = map.transform as RectTransform;
    fingerScript = gameObject.GetComponent<FingersScript>();

    //CreateTapGesture();
    //CreateDoubleTapGesture();
    //CreateSwipeGesture();
    CreatePanGesture();
    CreateScaleGesture();
    //CreateRotateGesture();
    //CreateLongPressGesture();

    //tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;

    // Pan rotate and scale can happen simultaneously.
    //panGesture.AllowSimultaneousExecution(scaleGesture);
    //panGesture.AllowSimultaneousExecution(rotateGesture);
    //scaleGesture.AllowSimultaneousExecution(rotateGesture);
  }


  private void CreateLongPressGesture()
  {

  }

  //private void CreateRotateGesture()
  //{
  //  rotateGesture = new RotateGestureRecognizer();
  //  rotateGesture.StateUpdated += RotateGestureCallback;
  //  fingerScript.AddGesture(rotateGesture);
  //}

  //private void RotateGestureCallback(GestureRecognizer gesture)
  //{
  //    //Debug.Log(rotateGesture.RotationRadiansDelta);
  //  if (gesture.State == GestureRecognizerState.Executing)
  //  {
  //    rectTransform.Rotate(0f, 0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg * rotationSpeed);
  //  }
  //}

  private void CreateScaleGesture()
  {
    scaleGesture = new ScaleGestureRecognizer();
    scaleGesture.StateUpdated += ScaleGestureCallback;
    fingerScript.AddGesture(scaleGesture);
  }

  private void ScaleGestureCallback(GestureRecognizer gesture)
  {
    if (rectTransform == null) return;
    //Debug.Log(gesture);
    if (gesture.State == GestureRecognizerState.Executing)
    {
      // Don't allow getting bigger if at max size, or smaller if at min size.
      if (rectTransform.localScale.x > maxSize && scaleGesture.ScaleMultiplier > 1)
      {
        return;
      }
      if (rectTransform.localScale.x < minSize && scaleGesture.ScaleMultiplier < 1)
      {
        return;
      }
      //rectTransform.sizeDelta *= scaleGesture.ScaleMultiplier;
      rectTransform.localScale *= scaleGesture.ScaleMultiplier;
    }
  }

  private void CreatePanGesture()
  {
    panGesture = new PanGestureRecognizer();
    panGesture.StateUpdated += PanGestureCallback;
    fingerScript.AddGesture(panGesture);
  }

  private void PanGestureCallback(GestureRecognizer gesture)
  {

    if (rectTransform == null) return;
    if (gesture.State == GestureRecognizerState.Executing)
    {
      //rectTransform.localPosition + rectTransform.sizeDelta.y / 2

      //var t = gesture.CurrentTrackedTouches;
      float deltaX = panGesture.DeltaX; // 10.0f;
      float deltaY = panGesture.DeltaY; // 10.0f;
      Vector3 pos = rectTransform.localPosition;

      var viewPortPos = Camera.main.WorldToViewportPoint(rectTransform.position);

      if (viewPortPos.x > 1.1 && deltaX > 0) return;
      if (viewPortPos.x < -.1 && deltaX < 0) return;
      if (viewPortPos.y > 1.35 && deltaY > 0) return;
      if (viewPortPos.y < -.35 && deltaY < 0) return;

      //rectTransform.pivot = new Vector2(1 - viewPortPos.x,  1 - viewPortPos.y);

      pos.x += deltaX;
      pos.y += deltaY;
      pos.z = 0;

      rectTransform.localPosition = pos;
    }
  }

  private void CreateSwipeGesture()
  {

  }

  private void CreateDoubleTapGesture()
  {

  }

  public void SetMap(GameObject map)
  {
    this.rectTransform = map.transform.GetChild(0).transform as RectTransform;
  }

  //public void SetRectTransform()
  //{
  //  rectTransform = map.transform as RectTransform;
  //}

  //public void SetRectTransform(RectTransform item)
  //{
  //  Debug.Log("Setting rect transform for " + item.name);
  //  rectTransform = item;
  //}

  //private void CreateTapGesture()
  //{
  //  tapGesture = new TapGestureRecognizer();
  //  tapGesture.StateUpdated += TapGestureCallback;
  //  fingerScript.AddGesture(tapGesture);
  //}

  //private void TapGestureCallback(GestureRecognizer gesture)
  //{
  //  if (gesture.State == GestureRecognizerState.Ended)
  //  {
  //    GestureTouch t = gesture.CurrentTrackedTouches[0];
  //    Debug.Log("Tapped at" + t.X + t.Y);
  //  }
  //}
}