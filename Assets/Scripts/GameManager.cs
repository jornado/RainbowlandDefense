using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        // end of the game!
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("GAME OVER MAN!");
    }
}
