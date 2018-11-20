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
    public Transform projectileSpawnLeft;
    public Transform projectileSpawnRight;
    public Transform imageTarget;

    private float nextShoot = 0.0f;
    private bool shootFromLeft = true;

    private Transform currentShotOrigin;

    public void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            aimPoint.position = hit.point;
            projectileSpawnLeft.LookAt(aimPoint);
            projectileSpawnRight.LookAt(aimPoint);
        };
    }

    public void shoot()
    {
        if (Time.time > nextShoot)
        {
            if (shootFromLeft)
            {
                currentShotOrigin = projectileSpawnLeft;
                shootFromLeft = false;
            }
            else
            {
                currentShotOrigin = projectileSpawnRight;
                shootFromLeft = true;
            }

            nextShoot = Time.time + rateOfFire;
            Instantiate(projectile, currentShotOrigin.position, currentShotOrigin.rotation, imageTarget);
        }
    }
}
