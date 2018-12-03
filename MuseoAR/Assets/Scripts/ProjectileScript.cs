﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @puupertti 2018-11-06
/// 
/// A script for handling shot projectiles.
/// </summary>
public class ProjectileScript : MonoBehaviour {

    public float speed = 2.0f;
    public float lifetime = 4.0f;

    private Rigidbody rb;
    private float aliveTime;

    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(this.gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            other.GetComponent<EnemyScript>().die();
            Destroy(this.gameObject);
        }

    }


}
