using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public float tickSpeed = 0.3f;  //How long between movement ticks
    public float movementSpeed = 7f;
    public int movementTicks = 4; //How many ticks the enemies move to one direction
    public bool TouchingPlane
    {
        get
        {
            return _touchingPlane;
        }
    }

    private float gameOverHeight; //The z cordinate which causes game over when reached by enemy
    private bool moveRight;
    private bool _touchingPlane = false;
    private float radius;   //Distance from the center of the mesh to it's bottom

    private void Awake()
    {
        moveRight = true;
        StartCoroutine(MoveEnemy());
        radius = GetComponent<MeshFilter>().mesh.bounds.size.z/2;
        gameOverHeight = GameObject.Find("GameOverPlane").transform.localPosition.z;
    }

    private void Update()
    {
        //transform.LookAt(Camera.main.transform, Camera.main.transform.up);
    }

    /// <summary>
    /// Tells the enemy to die. Enemy then informs the manager to add points to score.
    /// </summary>
    public void die()
    {
        //Turn the enemy red
        MeshRenderer m_rend = GetComponent<MeshRenderer>();
        m_rend.material.color = Color.red;

        Destroy(this.gameObject, 1.0f);

        //TODO: Add points to score
    }

    /// <summary>
    /// Moves this object left and right and towards the camera.
    /// Destroys object if it is too close to the camera.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveEnemy()
    {
        yield return new WaitForSeconds(tickSpeed);

        // Switch for left and right movement
        for(int i = 0; i <= movementTicks; i++)
        {
            if (moveRight)
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            else
                transform.Translate(-Vector3.right * Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(tickSpeed);
        }        
        moveRight = !moveRight;

        // Downwards
        transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
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
