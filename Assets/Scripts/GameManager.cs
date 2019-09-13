using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;

    // Make sure game is not over when scene is loaded
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
            EndGame();

        if (GameIsOver)
            return;

        // end of the game!
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    // show the Game Over menu
    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
