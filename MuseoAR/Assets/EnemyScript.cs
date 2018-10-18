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
    /// Liikuttaa vihollisobjektia jossa skripti on kiinni
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveEnemy()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < minDistance)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(waitTime);
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
