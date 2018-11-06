using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @haeejuut 10.17.2018
/// @PuuperttiRuma 2018-11-01
/// 
/// Spawns enemies and manages a list of them. Can reset.
/// </summary>
public class ManagerScript : MonoBehaviour {

    public GameObject EnemyPrefab;
    public int enemiesOnRow = 4;
    public int enemyRows = 2;
    public float enemySpacing = 0.5f;
    
    public GameObject[] EnemyList;
    private GameObject ImageTarget;

	// Use this for initialization
	void Start () {
        ImageTarget = gameObject.transform.parent.gameObject;
        SpawnEnemies();
	}

    /// <summary>
    /// Spawns enemyprefabs on incremental x-positions and adds the instantiated enemies
    /// into the EnemyList array.
    /// </summary>
    private void SpawnEnemies()
    {
        //Creates a new list of enemies to ensure that the list is of correct size.
        //Better functionality would be to have a dynamic list object instead of array.
        EnemyList = new GameObject[enemiesOnRow * enemyRows + 1];

        //Calculate the first spawn point
        float x = -(enemiesOnRow-1)*enemySpacing/2.0f;
        float y = 0.0f;
        float z = 0.0f;

        for (int j = 0; j < enemyRows; j++)
        {
            //Spawn a row
            for (int i = 0; i < enemiesOnRow; i++)
            {
                GameObject enemyGO = Instantiate<GameObject>(EnemyPrefab);
                EnemyList[j * enemiesOnRow + i] = enemyGO;
                enemyGO.transform.localPosition = new Vector3(x, y, z);
                x += enemySpacing;
                enemyGO.transform.localScale = new Vector3(.2f, .2f, .2f);
                // Set as children for ImageTarget, for AR's sake
                enemyGO.transform.SetParent(ImageTarget.transform);
            }
            //Reset x to the start of a row
            x = -(enemiesOnRow - 1) * enemySpacing / 2.0f;
            //Update the y and z for the next row
            y += enemySpacing;            
            z += enemySpacing;   //For some reason you have to add to make z smaller
        }
    }

    /// <summary>
    /// Resets the game to the starting position using the values given in initialization.
    /// </summary>
    public void ResetGame()
    {
        //Destroy all remaining enemies
        foreach (var enemy in EnemyList)
        {
            Destroy(enemy);
        }
        //SpawnEnemies();
    }
}
