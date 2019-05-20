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

    float score = 0f;
    float speedy = 1.8f;
    float speedz = 3.8f;
    float gravity = 0.03f;
    string action = "";
    string usertext = "";
    float timer;
    float time;
    float hit;

    // Start is called before the first frame update
    void Start() {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            if(action=="") {
                action = "throw";
                timer = Time.time;
                StartCoroutine(SmoothMove(hand, hand.transform.position, new Vector3(-47f, -17.3f, 1.1f), 1f));
                StartCoroutine(SmoothRot(ball, ball.transform.rotation, new Quaternion(1f, 0f, 0f, 1f), 1f));
                print(ball.transform.rotation);
            } else if(action=="throw") {
                StartCoroutine(SmoothRot(bat, bat.transform.rotation, new Quaternion(0.5f, 0.5f, 0.5f, 0.5f), 0.2f));
            }
        }

        time = Time.time - timer;

        if (ball.transform.position.z/5 > score) {
            score = Mathf.Floor(ball.transform.position.z/5);
            highscore.text = "Ennätys: " + score + " metriä";
        }

        if (action=="throw") {
            speedy -= gravity;
            if (ball.transform.position.y > -44) ball.transform.position += new Vector3(0, speedy, 0);
            if (time > 2) {
                hit = Mathf.Abs(bat.transform.rotation.y);
                if (hit < 0.3f) {
                    action = "hit";
                    speedz = (0.35f - hit) * 30;
                    speedy = (0.35f - hit) * 5;
                    timer = Time.time;
                } else action = "miss";
            }
        } else if (action == "miss") {
            speedy -= gravity;
            if (ball.transform.position.y > -44) ball.transform.position += new Vector3(0, speedy, 0);
            if (time > 3) Reset();
        } else if (action == "hit") {
            speedy -= gravity;
            speedz -= gravity;
            if (ball.transform.position.y > -44) ball.transform.position += new Vector3(0, speedy, speedz);
            if (time > 3) Reset();
        } else if (action == "reset") {
            if (time > 1) action = "";
        }


    }

    private void Reset() {
        action = "reset";
        speedy = 1.8f;
        timer = Time.time;
        StartCoroutine(SmoothMove(ball, ball.transform.position, new Vector3(0f,-15f,0f), 0.5f));
        StartCoroutine(SmoothRot(ball, ball.transform.rotation, new Quaternion(0f, 0f, 0f, 1f), 1f));
        StartCoroutine(SmoothMove(hand, hand.transform.position, new Vector3(-7f, -17.3f, 1.1f), 0.5f));
        StartCoroutine(SmoothRot(bat, bat.transform.rotation, new Quaternion(-0.5f, -0.5f, 0.5f, 0.5f), 0.5f));
    }

    IEnumerator SmoothMove(GameObject obj, Vector3 start, Vector3 end, float seconds) {
        var t = 0.0f;
        while (t <= 1.0) {
            yield return new WaitForSeconds(0.02f);
            obj.transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += 0.02f / seconds;
        }
    }

    IEnumerator SmoothRot(GameObject obj, Quaternion start, Quaternion end, float seconds) {
        var t = 0.0f;
        while (t <= 1.0) {
            yield return new WaitForSeconds(0.02f);
            obj.transform.rotation = Quaternion.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            t += 0.02f / seconds;
        }
    }

}
