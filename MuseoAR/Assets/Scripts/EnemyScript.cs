using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public float tickSpeed = 0.8f;  //How long between movement ticks
    public float movementSpeed = 7f;
    public int movementTicks = 6; //How many ticks the enemies move to one direction
    public GameObject deathEffect;
    public int pointsValue = 200;

    private Rigidbody rb;
    private InvadersManagerScript _manager;
    private GameObject imageTarget;

    private float gameOverHeight; //The z cordinate which causes game over when reached by enemy    
    //Käytetäänkö radiusta enää mihinkään?
    private float radius;   //Distance from the center of the mesh to it's bottom    
    private bool moving = true;
    private bool moveRight;
    private int movesToDirection = 0;
    private float timeSinceLastMove = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveRight = true;
        //StartCoroutine(MoveEnemy());
        radius = GetComponent<MeshFilter>().mesh.bounds.size.z/2; 
        imageTarget = GameObject.Find("ImageTarget");
        _manager = imageTarget.GetComponent<InvadersManagerScript>();
    }

    private void FixedUpdate()
    {
        timeSinceLastMove += Time.deltaTime;
        if(moving && timeSinceLastMove >= tickSpeed)
        {
            moveEnemy();
        }
    }

    /// <summary>
    /// Moves this enemy left and right and towards the game over boxes.
    /// </summary>
    private void moveEnemy()
    {
        timeSinceLastMove = 0;
        if (moveRight)
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * movementSpeed);
        else
            rb.MovePosition(transform.position - Vector3.right * Time.deltaTime * movementSpeed);

        movesToDirection++;
        if (movesToDirection >= movementTicks)
        {
            movesToDirection = 0;
            moveRight = !moveRight;

            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * movementSpeed);
        }
    }

    /// <summary>
    /// Tells the enemy to die. Enemy then informs the manager to add points to score.
    /// </summary>
    public void die()
    {
        addPoints();
        explode();

        //Destroy the enemy
        _manager.RemoveEnemyFromList(gameObject);
        Destroy(this.gameObject);

        _manager.CheckIfStageCompleted();
    }

    private void explode()
    {
        //Create and animate the enemy's death effect
        Transform transform = GetComponent<Transform>();
        GameObject explosion = Instantiate(deathEffect, transform.position, transform.rotation, imageTarget.transform);
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        ps.Play();
        var em = ps.emission;
        em.enabled = true;

        //Destroy the Particle System so it doesn't linger on indefinetely
        Destroy(explosion, 1.0f);
    }

    protected void addPoints()
    {
        _manager.Score += pointsValue;
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


    
}
