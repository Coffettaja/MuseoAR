using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseballManager : MonoBehaviour
{

    // include game objects
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject ball;
    [SerializeField] private Text gametitle;
    [SerializeField] private Text highscore;

    int score = 0;
    float speedy = 1.8f;
    float gravity = 0.03f;
    string action = "";
    string usertext = "";

    // Start is called before the first frame update
    void Start()
    {
        print("init");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(action=="")
            {
                action = "throw";
                StartCoroutine(SmoothMove(hand, hand.transform.position, new Vector3(-50, 0, 0), 1f));
            } else if(action=="throw")
            {
                action = "hit";
                StartCoroutine(SmoothRot(bat, bat.transform.rotation, new Quaternion(-0.5f, 0.5f, 0.5f, 0.5f), 0.3f));
                StartCoroutine(CheckHit());
            }
            //highscore.text = "highscore: " + score;
            //print(bat.transform.rotation);
        }

        if (action!="")
        {
            speedy -= gravity;
            if(ball.transform.position.y>-44) ball.transform.position += new Vector3(0, speedy, 0);

        }

    }

    IEnumerator SmoothMove(GameObject obj, Vector3 start, Vector3 end, float time)
    {
        var t = 0.0f;
        while (t <= 1.0)
        {
            yield return new WaitForSeconds(0.01f);
            obj.transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += 0.01f/time;
        }
    }

    IEnumerator SmoothRot(GameObject obj, Quaternion start, Quaternion end, float time)
    {
        var t = 0.0f;
        while (t <= 1.0)
        {
            yield return new WaitForSeconds(0.01f);
            obj.transform.rotation = Quaternion.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += 0.01f/time;
        }
    }

    IEnumerator CheckHit()
    {
        print(Time.time);
        yield return new WaitForSeconds(0.4f);
        print(Time.time);
        print(ball.transform.position.y);
    }

}
