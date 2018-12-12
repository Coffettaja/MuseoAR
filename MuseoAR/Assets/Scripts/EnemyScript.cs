using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public float tickSpeed = 0.8f;  //How long between movement ticks
    public float movementSpeed = 7f;
    public int movementTicks = 6; //How many ticks the enemies move to one direction

    private Rigidbody rb;
    private ParticleSystem deathAnim;

    private float gameOverHeight; //The z cordinate which causes game over when reached by enemy
    private bool moveRight;
//    private bool _touchingPlane = false;
    private float radius;   //Distance from the center of the mesh to it's bottom
    private InvadersManagerScript _manager;

    private int movesToDirection = 0;
    private float timeSinceLastMove = 0;

    private bool moving = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveRight = true;
        //StartCoroutine(MoveEnemy());
        radius = GetComponent<MeshFilter>().mesh.bounds.size.z/2;
        _manager = GameObject.Find("ImageTarget").GetComponent<InvadersManagerScript>();
        deathAnim = GetComponent<ParticleSystem>();
    }


    private void FixedUpdate()
    {
        timeSinceLastMove += Time.deltaTime;
        if(moving && timeSinceLastMove >= tickSpeed)
        {
            timeSinceLastMove = 0;
            if (moveRight)
                rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * movementSpeed);
            //transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            else
                rb.MovePosition(transform.position - Vector3.right * Time.deltaTime * movementSpeed);
            //transform.Translate(-Vector3.right * Time.deltaTime * movementSpeed);

            movesToDirection++;
            if (movesToDirection >= movementTicks)
            {
                movesToDirection = 0;
                moveRight = !moveRight;

                transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * movementSpeed);
            }
        }
    }

    /// <summary>
    /// Tells the enemy to die. Enemy then informs the manager to add points to score.
    /// </summary>
    public void die()
    {
        ////Turn the enemy red
        //MeshRenderer m_rend = GetComponent<MeshRenderer>();
        //m_rend.material.color = Color.red;

        addPoints();
        _manager.RemoveEnemyFromList(gameObject);

        //Explodes enemy
        Debug.Log("Osu");
        deathAnim.Play();
        Destroy(this.gameObject);

        _manager.CheckIfStageCompleted();
    }

    private void addPoints()
    {
        _manager.Score += 500;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GameOverPlane")
        {
            _manager.GameOver();
        }
    }

    public void Stop()
    {
        moving = false;
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
                rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * movementSpeed);
            //transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            else
                rb.MovePosition(transform.position -Vector3.right * Time.deltaTime * movementSpeed);
            //transform.Translate(-Vector3.right * Time.deltaTime * movementSpeed);
            yield return new WaitForSeconds(tickSpeed);
        }
        moveRight = !moveRight;

        // Downwards
        transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * movementSpeed);
        StartCoroutine(MoveEnemy());       
        
    }
    
}
