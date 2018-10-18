using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool destroyed = false;
    public float waitTime = 3f;

    private float speed = 5f;
    private float minDistance = 1.0f;
    private bool swi;

    private void Start()
    {
        swi = true;
        StartCoroutine(MoveEnemy());
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, Camera.main.transform.up);
    }

    /// <summary>
    /// Moves this object left and right and towards the camera.
    /// Destroys object if it is too close to the camera.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveEnemy()
    {
        // Distance check
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < minDistance)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(waitTime);

        // Switch for left and right movement
        if (swi)
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        else
            transform.Translate(-Vector3.right * Time.deltaTime * speed);

        yield return new WaitForSeconds(waitTime);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed + 4.0f));
        swi = !swi;
        
        StartCoroutine(MoveEnemy());
    }
}
