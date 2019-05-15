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

    //Crosshair

    public bool drawCrosshair = true;
    public Color crosshairColor = Color.white; // Crosshair color
    public float width = 1; // Crosshair width 
    public float height = 3; // Crosshair height

    [System.Serializable]
    public class spreading
    {
        public float sSpread = 20; // Adjust for bigger or smaller crosshair
        public float maxSpread = 40;
        public float minSpread = 20;
        public float spreadPerSecond = 30;
        public float decreasePerSecond = 25;
    }

    public spreading spread = new spreading();

    Texture2D tex;
    float newHeight;
    GUIStyle lineStyle;

    void Awake ()
    {
        tex = new Texture2D(1, 1);
        lineStyle = new GUIStyle();
        lineStyle.normal.background = tex;
    }

    void OnGUI ()
    {
        Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.width / 3);
        float screenRatio = Screen.height / 100;

        newHeight = height * screenRatio * 2;

        if (drawCrosshair)
        {
            GUI.Box(new Rect(centerPoint.x - (width / 2), centerPoint.y - (newHeight + spread.sSpread), width*4, newHeight), GUIContent.none, lineStyle);
            GUI.Box(new Rect(centerPoint.x - (width / 2), (centerPoint.y + spread.sSpread), width*4, newHeight), GUIContent.none, lineStyle);
            GUI.Box(new Rect((centerPoint.x + spread.sSpread), (centerPoint.y - (width / 2)), newHeight, width*5), GUIContent.none, lineStyle);
            GUI.Box(new Rect(centerPoint.x - (newHeight + spread.sSpread), (centerPoint.y - (width / 2)), newHeight, width*5), GUIContent.none, lineStyle);
        }

        if (Input.GetButton("Fire1"))
        {
            spread.sSpread += spread.spreadPerSecond * Time.deltaTime * 4; // Increment the spread
            shoot();
        }

        spread.sSpread -= spread.decreasePerSecond * Time.deltaTime; // Decrement the spread
        spread.sSpread = Mathf.Clamp(spread.sSpread, spread.minSpread, spread.maxSpread);
    }

    // Apply color to crosshair
    void SetColor(Texture2D myTexture, Color myColor)
    {
        for (int y = 0; y < myTexture.height; y++)
        {
            for (int x = 0; x < myTexture.width; x++)
                myTexture.SetPixel(x, y, myColor);
            myTexture.Apply();
        }
    }

    //end of crosshair

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
