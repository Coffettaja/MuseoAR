using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

/// <summary>
/// @haeejuut 10.17.2018
/// @PuuperttiRuma 2018-11-01
/// 
/// Spawns enemies and manages a list of them. Can reset.
/// </summary>
public class InvadersManagerScript : MonoBehaviour, ITrackableEventHandler
{

    public GameObject EnemyPrefab;
    public GameObject BonusEnemyPrefab;
    public GameObject GameOverPlanePrefab;
    public int enemiesOnRow = 4;
    public int enemyRows = 2;
    public float enemySpacing = 0.5f;
    public float enemyStartY = 2.0f;
    public float gameOverBlockHeight = 0.0f;
    public Transform SpawnPoint;
    public Transform BonusEnemySpawnpoint;

    public GameObject[] EnemyList;

    private int _score;
    private int m_level = 0;
    public static float factor = 0;

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _scoreText.text = "" + _score;
        }
    }

    private float m_bonusSpawnTime;

    private GameObject imageTarget;
    private GameObject _gameOverPlane;
    private bool enemiesSpawned = false;
    private bool gameOver = false;


    private TrackableBehaviour _trackableBehaviour;
    private GameObject _gameOverPopup;
    private GameObject _outOfFocusPopup;
    private Text _scoreText;

    // Use this for initialization
    void Start()
    {
        imageTarget = gameObject;
        _gameOverPopup = GameObject.Find("GameOverPopup");
        _gameOverPopup.SetActive(false);
        _trackableBehaviour = GetComponent<TrackableBehaviour>();
        _outOfFocusPopup = GameObject.Find("OutOfFocusImg");
        _outOfFocusPopup.SetActive(false);
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    //If the marker is found for the first time, starts the game
    //otherwise display the not-tracked-icon.
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        Debug.Log(newStatus);        
        if (newStatus == TrackableBehaviour.Status.TRACKED)
        {
            _outOfFocusPopup.SetActive(false);
            if (!enemiesSpawned)
            {
                enemiesSpawned = true;
                SpawnEnemies();
                SpawnGameOverPlane();
            }
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND && !gameOver)
        {
            _outOfFocusPopup.SetActive(true);
        }
    }

    private void SpawnGameOverPlane()
    {
        //Spawn the "Game Over" blocks
        GameObject blocks = Instantiate<GameObject>(GameOverPlanePrefab, SpawnPoint);
        blocks.name = "GameOverPlane";
        blocks.transform.localPosition += new Vector3(0, gameOverBlockHeight, 0);
    }

    /// <summary>
    /// Spawns enemyprefabs on incremental x-positions and adds the instantiated enemies
    /// into the EnemyList array.
    /// </summary>
    private void SpawnEnemies()
    {
        int enemyRowsOnLevel = (int)enemyRows + (m_level / 2);
        //Creates a new list of enemies to ensure that the list is of correct size.
        //Better functionality would be to have a dynamic list object instead of array.
        EnemyList = new GameObject[enemiesOnRow * enemyRowsOnLevel + 1];

        //Calculate the first spawn point
        float x = -(enemiesOnRow - 1) * enemySpacing / 2.0f;
        float y = enemyStartY;
        float z = 0;

        for (int j = 0; j < enemyRowsOnLevel; j++)
        {
            //Spawn a row
            for (int i = 0; i < enemiesOnRow; i++)
            {
                GameObject enemyGO = Instantiate<GameObject>(EnemyPrefab, SpawnPoint);
                //enemyGO.GetComponent<EnemyScript>().tickSpeed -= (((float)m_level) * 0.1f);
                enemyGO.transform.localPosition += new Vector3(x, y, z);
                enemyGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                EnemyList[j * enemiesOnRow + i] = enemyGO;
                x += enemySpacing;
                enemyGO.transform.localScale = new Vector3(.2f, .2f, .2f);
            }
            //Reset x to the start of a row
            x = -(enemiesOnRow - 1) * enemySpacing / 2.0f;
            //Update the y and z for the next row
            y += enemySpacing;
            //z += enemySpacing;   //For some reason you have to add to make z smaller
        }
        
        SpawnBonusEnemy();
    }

    private void SpawnBonusEnemy()
    {
        //Vector3 bonusSpawnDisplacement = Vector3.back + (Vector3.left * 3);
        GameObject bonus = Instantiate<GameObject>(BonusEnemyPrefab, SpawnPoint);
        bonus.transform.localPosition += BonusEnemySpawnpoint.localPosition;
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
        m_level = 0;
        Score = 0;
        factor = 0;
        SpawnEnemies();
        _gameOverPopup.SetActive(false);
        //SceneManager.LoadScene("invaders");
    }
    //Tell all the enemies to stop and show the popup screen about game over
    public void GameOver()
    {
        gameOver = true;
        foreach (GameObject go in EnemyList)
        {
            //Make sure to ignore empty indexes
            if (go != null)
            {
                go.GetComponent<EnemyScript>().Stop();
            }
        }
        _gameOverPopup.SetActive(true);
        Text text = _gameOverPopup.transform.Find("Text").GetComponent<Text>();
        text.text = "Game Over! Final Score: " + _score;

    }

    public void CheckIfStageCompleted()
    {
        bool stageCompleted = true;
        foreach (GameObject go in EnemyList)
        {
            if (go != null)
            {
                stageCompleted = false;
                break;
            }
        }

        if (stageCompleted)
        {
            Debug.Log("Stage completed!");
            m_level++;
            factor = factor + 0.2f; // Increase time that will be reduced from the tickspeed.
            Invoke("SpawnEnemies", 1.0f);
            //Invoke("SpawnBonusEnemy", 10);
        }
    }

    /// <summary>
    /// Removes gameobject from enemylist.
    /// Called by enemyscript when an enemy dies.
    /// </summary>
    public void RemoveEnemyFromList(GameObject deadObject)
    {
        for (int i = 0; i < EnemyList.Length; i++)
        {
            if (EnemyList[i] == deadObject)
            {
                EnemyList[i] = null;
            }
        }
    }



}
