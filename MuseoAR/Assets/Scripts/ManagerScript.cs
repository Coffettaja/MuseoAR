using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @haeejuut 10.17.2018
/// At the moment only manages spawning enemies and keeping a list of spawned objects.
/// </summary>
public class ManagerScript : MonoBehaviour {

    public GameObject EnemyPrefab;

    private int amount;
    private GameObject[] EnemyList;
    private GameObject ImageTarget;

	// Use this for initialization
	void Start () {
        amount = 4;
        ImageTarget = gameObject.transform.parent.gameObject;
        EnemyList = new GameObject[amount + 1];
        SpawnEnemies(amount);
	}

    /// <summary>
    /// Spawner-dummy, spawns enemyprefabs on incremental x-positions.
    /// Sets instantiated enemies into the EnemyList array, if saving them is
    /// needed at any point in the future.
    /// Takes as a parameter the amount of enemies you wish to spawn.
    /// </summary>
    /// <param name="amount"></param>
    private void SpawnEnemies(int amount)
    {
        float x = -1;
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyGO = EnemyList[i] = Instantiate<GameObject>(EnemyPrefab);
            enemyGO.transform.localPosition = new Vector3(x, 0, 0);
            x += .5f;
            enemyGO.transform.localScale = new Vector3(.2f, .2f, .2f);
            // Set as children for ImageTarget, for AR's sake
            enemyGO.transform.SetParent(ImageTarget.transform);
        }
    }
}
