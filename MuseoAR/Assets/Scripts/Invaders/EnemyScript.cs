using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    
    public GameObject deathEffect;
    public int pointsValue;
    protected InvadersManagerScript _manager;
    private GameObject imageTarget;    

    private void Awake()
    {
        imageTarget = GameObject.Find("ImageTarget");
        _manager = imageTarget.GetComponent<InvadersManagerScript>();
    }

    /// <summary>
    /// Tells the enemy to die. Enemy then informs the manager to add points to score.
    /// </summary>
    public void Die()
    {
        AddPoints();
        Explode();

        //Destroy the enemy
        _manager.RemoveEnemyFromList(gameObject);
        Destroy(this.gameObject);

        _manager.CheckIfStageCompleted();
    }

    public void Explode()
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

    public void AddPoints()
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
        var moveScript = GetComponent<EnemyMovement>();
        moveScript.Stop();
    }
}
