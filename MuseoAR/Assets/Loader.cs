using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.gameManagerInstance == null)
        {
            Instantiate(gameManager);
        }
    }
}
