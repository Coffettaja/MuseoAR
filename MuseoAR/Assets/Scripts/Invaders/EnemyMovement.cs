using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float tickSpeed = 0.8f;  //How long between movement ticks
    public float movementSpeed = 7f;
    public int movementTicks = 6; //How many ticks the enemies move to one direction
    private float change = 0;

    private Rigidbody rb;
    private bool moving = true;
    private bool moveRight;
    private int movesToDirection = 0;
    private float timeSinceLastMove = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveRight = true;
    }

    private void FixedUpdate()
    {
        float change = InvadersManagerScript.factor; // Change in tickspeed due to clearing levels.
        timeSinceLastMove += Time.deltaTime;
        if (moving && timeSinceLastMove >= tickSpeed - change)
        {
            moveEnemy();
        }
    }

    public void Stop()
    {
        moving = false;
    }

    // Update is called once per frame
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

}
