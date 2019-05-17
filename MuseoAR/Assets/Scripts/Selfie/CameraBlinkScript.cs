using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlinkScript : MonoBehaviour {

	public void Blink()
  {
    gameObject.SetActive(true);
    StartCoroutine("BlinkEffect");
  }

  private IEnumerator BlinkEffect()
  {
    yield return new WaitForSeconds(.15f);
    gameObject.SetActive(false);
  }
}
