using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelfieSceneButton : MonoBehaviour {

    RectTransform rt;
    float shakeSpeed = 1f;
    GameObject noticeBang;

	// Use this for initialization
	void Start ()
    {
        noticeBang = transform.Find("NoticeBang").gameObject;
        noticeBang.SetActive(false);
        rt = GetComponent<RectTransform>();
        //Debug.Log(PlayerPrefs.GetString("DecorUnlock"));
        GetComponent<Button>().onClick.AddListener(LoadSelfieScene);
        if(PlayerPrefs.GetString("DecorUnlock") != "")
        {
            //noticeBang.SetActive(true);
            //StartCoroutine(ShakeButton(20));
            //PlayerPrefs.DeleteKey("DecorUnlock");
        }        
	}
	
	void LoadSelfieScene()
    {

    SceneManager.LoadScene("Selfie");
    }

    private IEnumerator ShakeButton(int rot)
    {
        float t = Time.time;
        float rotGoal = rot;
        while(rotGoal > 1)
        {
            Vector3 eulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 75, rotGoal) - (rotGoal / 2));
            rt.rotation = Quaternion.Euler(eulerAngles);
            rotGoal -= (Time.time - t)/5;
            yield return null;
        }        
    }
}
