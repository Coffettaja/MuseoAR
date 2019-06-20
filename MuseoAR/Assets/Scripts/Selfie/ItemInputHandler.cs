using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System;

public class ItemInputHandler : MonoBehaviour {

  private float maxSize = 5.5f;
  private float minSize = .15f;
  private float rotationSpeed = 2.5f;

  public bool Dragging { get; set; }

  private FingersScript fingerScript;

  private TapGestureRecognizer tapGesture;
 
  //private SwipeGestureRecognizer swipeGesture;
  //private PanGestureRecognizer panGesture;
  private RotateGestureRecognizer rotateGesture;
  private ScaleGestureRecognizer scaleGesture;
  //private LongPressGestureRecognizer longPressGesture;

  private RectTransform rectTransform;

	// Use this for initialization
	void Start () {
    Dragging = false;
    // Set some default rectTransform to avoid checking if some exists eveytime before any action.
    rectTransform = transform.Find("DummyItem").transform as RectTransform; 
    fingerScript = gameObject.GetComponent<FingersScript>();

    //CreateTapGesture();
    //CreateDoubleTapGesture();
    //CreateSwipeGesture();
    //CreatePanGesture();
    CreateScaleGesture();
    CreateRotateGesture();
    //CreateLongPressGesture();

    //tapGesture.RequireGestureRecognizerToFail = doubleTapGesture;

    // Pan rotate and scale can happen simultaneously.
    //panGesture.AllowSimultaneousExecution(scaleGesture);
    //panGesture.AllowSimultaneousExecution(rotateGesture);
    scaleGesture.AllowSimultaneousExecution(rotateGesture);
	}

  private void CreateLongPressGesture()
  {
    
  }

  private void CreateRotateGesture()
  {
    rotateGesture = new RotateGestureRecognizer();
    rotateGesture.StateUpdated += RotateGestureCallback;
    fingerScript.AddGesture(rotateGesture);
  }

  private void RotateGestureCallback(GestureRecognizer gesture)
  {
    if (gesture.State == GestureRecognizerState.Executing)
    {
      rectTransform.Rotate(0f, 0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg * rotationSpeed);
    }
  }

  private void CreateScaleGesture()
  {
    scaleGesture = new ScaleGestureRecognizer();
    scaleGesture.StateUpdated += ScaleGestureCallback;
    fingerScript.AddGesture(scaleGesture);
  }

  private void ScaleGestureCallback(GestureRecognizer gesture)
  {
    if (gesture.State == GestureRecognizerState.Executing)
    {
      // Don't allow getting bigger if at max size, or smaller if at min size.
      if (rectTransform.localScale.x > maxSize && scaleGesture.ScaleMultiplier > 1) return;
      if (rectTransform.localScale.x < minSize && scaleGesture.ScaleMultiplier < 1) return;
      rectTransform.localScale *= scaleGesture.ScaleMultiplier;
    }
  }

  private void CreatePanGesture()
  {
    
  }

  private void CreateSwipeGesture()
  {
    
  }

  private void CreateDoubleTapGesture()
  {
    
  }

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

    /// <summary>
    /// Sets the item that the gestures apply to.
    /// </summary>
    /// <param name="item"></param>
  public void SetRectTransform(RectTransform item)
  {
    //Debug.Log("Setting rect transform for " + item.name);
    rectTransform = item;
  }

  public RectTransform GetDraggedRectTransform()
  {
    return rectTransform;
  }
}
