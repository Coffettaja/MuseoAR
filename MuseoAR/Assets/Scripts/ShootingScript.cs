using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// @puupertti 2018-11-06
/// Script for handling the shooting. Could be moved within another script.
/// </summary>
public class ShootingScript : MonoBehaviour {

    public float rateOfFire = 2.0f;
    public GameObject projectile;
    public Transform projectileSpawn;

    private float nextShoot = 0.0f;

    public void shoot()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + rateOfFire;
            Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}
