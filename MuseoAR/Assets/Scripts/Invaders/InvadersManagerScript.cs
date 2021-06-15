using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// @haeejuut 10.17.2018
/// @PuuperttiRuma 2018-11-01
/// 
/// Spawns enemies and manages a list of them. Can reset.
/// </summary>
public class InvadersManagerScript : DefaultTrackableEventHandler//, ITrackableEventHandler
{

    public GameObject EnemyPrefab;
    public GameObject BonusEnemyPrefab;
    public GameObject GameOverPlanePrefab;
    public int enemiesOnRow = 4;
    public int enemyRows = 2;
    public float enemySpacing = 0.5f;
    public float enemyStartY = 0.0f;
    public float gameOverBlockHeight = 0.0f;
    public Transform SpawnPoint;
    public Transform BonusEnemySpawnpoint;

    public GameObject[] EnemyList;

    private int _score;
    private int m_level = 0;
    public static float factor = 0;

    public int Score {
        get { return _score; }
        set {
            _score = value;
            _scoreText.text = "" + _score;
        }
    }

    private float m_bonusSpawnTime;

    private GameObject imageTarget;
    private GameObject _gameOverPlane;
    private bool enemiesSpawned = false;
    public static bool gameOver = false;


    private TrackableBehaviour _trackableBehaviour;
    private GameObject _gameOverPopup;
    private GameObject _outOfFocusPopup;
    private Text _scoreText;
    private System.Action<TrackableBehaviour.StatusChangeResult> OnTrackableState;// = delegate{};
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
        OnTrackableState = OnTrackableStateChanged;
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableState);
        }
        gameOver = false;
      
    }

    //If the marker is found for the first time, starts the game
    //otherwise display the not-tracked-icon.
    public void OnTrackableStateChanged(TrackableBehaviour.StatusChangeResult change)//TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if ( !enemiesSpawned ) {
            enemiesSpawned = true;
            SpawnEnemies();
            SpawnGameOverPlane();
        }
        //Debug.Log(change.NewStatus);
        //if (change.NewStatus == TrackableBehaviour.Status.TRACKED ||
        //    change.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED||
        //    change.NewStatus == TrackableBehaviour.Status.LIMITED ) {
        //    _outOfFocusPopup.SetActive(false);
        //    if (!enemiesSpawned) {
        //        enemiesSpawned = true;
        //        SpawnEnemies();
        //        SpawnGameOverPlane();
        //    }
        //}
        //else if (change.PreviousStatus == TrackableBehaviour.Status.TRACKED &&
        //         change.NewStatus == TrackableBehaviour.Status.NO_POSE /*was: NOTFOUND*/ && !gameOver) {
        //    _outOfFocusPopup.SetActive(true);
        //}

        //THIS IS ORIGINAL CODE -->
        //Debug.Log(newStatus);        
        //if (newStatus == TrackableBehaviour.Status.TRACKED)
        //{
        //    _outOfFocusPopup.SetActive(false);
        //    if (!enemiesSpawned)
        //    {
        //        enemiesSpawned = true;
        //        SpawnEnemies();
        //        SpawnGameOverPlane();
        //    }
        //}
        //else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
        //         newStatus == TrackableBehaviour.Status.NOTFOUND && !gameOver)
        //{
        //    _outOfFocusPopup.SetActive(true);
        //}
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
        //Debug.Log("Spawning...");
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
                enemyGO.transform.localPosition += new Vector3(x, y, z);
               // enemyGO.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                EnemyList[j * enemiesOnRow + i] = enemyGO;
                x += enemySpacing;
                enemyGO.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            //Reset x to the start of a row
            x = -(enemiesOnRow - 1) * enemySpacing / 2.0f;
            //Update the y and z for the next row
            y += enemySpacing;
        }
        
        SpawnBonusEnemy();
    }

    private void SpawnBonusEnemy()
    {
        GameObject bonus = Instantiate<GameObject>(BonusEnemyPrefab, SpawnPoint);
        bonus.transform.localPosition += BonusEnemySpawnpoint.localPosition;
        bonus.transform.localScale = new Vector3(.5f, .5f, .5f);
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
        gameOver = false;
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
        GameControllerScript.Instance.AddPoints(_score, "invaders");

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
