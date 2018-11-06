using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool destroyed = false;
    public float waitTime = 0.3f;

    private float speed = 7f;
    private float minDistance = 1.0f;
    private bool swi;

    //Distance from the center of the mesh to it's bottom
    public float radius;
    //The z cordinate which causes game over when reached by enemy
    public float gameOverHeight;

    private bool _touchingPlane = false;
    public bool TouchingPlane
    {
        get
        {
            return _touchingPlane;
        }
    }

    private int movementRange = 4;

    private void Awake()
    {
        swi = true;
        StartCoroutine(MoveEnemy());
        radius = GetComponent<MeshFilter>().mesh.bounds.size.z/2;
        gameOverHeight = GameObject.Find("GameOverPlane").transform.localPosition.z;
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
        //if (Vector3.Distance(transform.position, Camera.main.transform.position) < minDistance)
        //{
        //    Destroy(gameObject);
        //}
        yield return new WaitForSeconds(waitTime);

        // Switch for left and right movement
        for(int i = 0; i <= movementRange; i++)
        {
            if (swi)
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            else
                transform.Translate(-Vector3.right * Time.deltaTime * speed);
            yield return new WaitForSeconds(waitTime);
        }
        
        swi = !swi;

        // Towards the player (camera)
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if(transform.localPosition.z - radius < gameOverHeight)
        {
            Debug.Log("Game Over Loser!");
        }
        else
        {
            StartCoroutine(MoveEnemy());
        }
        
        
    }
        
}
