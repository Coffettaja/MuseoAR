using UnityEngine;

public class BonusEnemyMovement : MonoBehaviour {

    public float movementSpeed;
    public float SinCurveMultiplier;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        moveEnemy();
	}

    // Moving bonus enemy in a wave-like pattern.
    private void moveEnemy()
    {
        Vector3 height = Mathf.Sin(Time.time * SinCurveMultiplier) * Vector3.up;
        Vector3 moveVector = height + Vector3.right;
        transform.Translate(moveVector * Time.deltaTime * movementSpeed, Space.World);
    }
}
