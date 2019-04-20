using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Handles the score 
/// </summary>
public class ScoreScript : MonoBehaviour {

  private int _score = 0;

  /// <summary>
  /// The current score of the player.
  /// </summary>
  public int Score {
    get
    {
      return _score;
    }
    private set {
      _score = value;
      UpdateScoreDisplay();
    }
  }

  #region Creating the singleton Instance.
  // Below has just been copied from GameControllerScript.cs
  // If there is need for any more Singletons, it would probably be
  // smart to create a Singleton base class and then inherit it 
  // when making a new singleton. 
  private static object _lock = new object();
  
  private static ScoreScript _instance;
  public static ScoreScript Instance
  {
  get
    {
      // Locks down the thread until the the Singleton instance has been created.
      lock (_lock)
      {
        if (_instance == null)
        {
          _instance = (ScoreScript)FindObjectOfType(
              typeof(ScoreScript));
          if (_instance == null)
          {
            GameObject singletonObject = new GameObject();
            _instance = singletonObject.AddComponent<ScoreScript>();
            singletonObject.name = typeof(ScoreScript).ToString() + " (Singleton)";
            DontDestroyOnLoad(singletonObject);
          }
        }
      }
      return _instance;
    }
  }
  #endregion

  /// <summary>
  /// Increases the score of the player by the amount specified in the parameter.
  /// </summary>
  /// <param name="amountToIncrease">By how many points the score is increased.</param>
  /// <returns>The new score value after the increase.</returns>
  public int IncreaseScoreBy(int amountToIncrease)
  {
    Score += amountToIncrease;
    return Score;
  }

  /// <summary>
  /// Decreases the score of the player by the amount specified in the parameter.
  /// </summary>
  /// <param name="amountToDecrease">By how many points the score is decreased.</param>
  /// <returns>The new score value after the decrease.</returns>
  public int DecreaseScoreBy(int amountToDecrease)
  {
    Score -= amountToDecrease;
    return Score;
  }

    /// <summary>
    /// Changes the score of the player to zero for the next player.
    /// </summary>
    public void ResetScore()
    {
        Score = 0;
    }

    /// <summary>
    /// If there exists a ScoreDisplay on the current scene, updates the display value to match
    /// the current score.
    /// </summary>
    public void UpdateScoreDisplay()
  {
    // The score display in the scene has to be marked with "ScoreDisplay" tag.
    // Although by using the ScoreDisplay prefab, it should set automatically.
    GameObject[] scoreDisplays = GameObject.FindGameObjectsWithTag("ScoreDisplay");

    // If there is no score display on the current scene, do nothing.
    if (scoreDisplays.Length == 0)
    {
      return;
    }

    // Note: currently it is possible to have only one score display on the scene at once.
    GameObject scoreDisplay = scoreDisplays[0];

    var scoreText = scoreDisplay.transform.GetComponent<TextMeshProUGUI>();

    // If the score display doesn't have a TextMeshProUGUI component, do nothing
    if (scoreText == null)
    {
      // I think it shouldn't be possible to get here if the ScoreDisplay prefab is used.
      // Not 100% sure though...
      return;
    }

    scoreText.SetText(Score.ToString());
  }

}
