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
    public Transform aimPoint;
    public Transform projectileSpawn;
    public Transform imageTarget;
    public GameObject aimPlane;

    private float nextShoot = 0.0f;
    
    public void Update()
    {
        //Raycasts to aimplane and then aims the projectile spawn to point at the aimpoint
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            aimPoint.position = hit.point;
            projectileSpawn.LookAt(aimPoint);
        };
    }

    public void shoot()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + rateOfFire;
            Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation, imageTarget);
        }
    }
}
