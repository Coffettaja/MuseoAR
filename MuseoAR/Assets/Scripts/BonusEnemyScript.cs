using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyScript : EnemyScript {

    public float MoveSpeed = 4.0f;

    private InvadersManagerScript m_manager;

	void Awake ()
    {
        pointsValue = 1000;
        m_manager = GameObject.Find("ImageTarget").GetComponent<InvadersManagerScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 height = Mathf.Sin(Time.time*2) * Vector3.up;
        Vector3 moveVector = height + Vector3.right;
        transform.Translate(moveVector * Time.deltaTime * MoveSpeed, Space.World);
	}
}
