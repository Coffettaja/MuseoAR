using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyScript : MonoBehaviour {

    public float MoveSpeed = 5.0f;

    private InvadersManagerScript m_manager;

	// Use this for initialization
	void Start ()
    {
        m_manager = GameObject.Find("ImageTarget").GetComponent<InvadersManagerScript>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 height = Mathf.Sin(Time.time) * Vector3.up;
        Vector3 moveVector = height + Vector3.right;
        transform.Translate(moveVector * Time.deltaTime * MoveSpeed, Space.World);
	}

    public void die()
    {
        MeshRenderer m_rend = GetComponent<MeshRenderer>();
        m_rend.material.color = Color.red;
        AddPoints();
        Destroy(this.gameObject, 1.0f);
    }

    private void AddPoints()
    {
        m_manager.Score += 100;
    }
}
