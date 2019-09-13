using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    /* Update player's remaining lives */
    void Update()
    {
        if (PlayerStats.Lives == 1)
        {
            livesText.text = PlayerStats.Lives + " LIFE";
        } else
        {
            livesText.text = PlayerStats.Lives + " LIVES";
        }
    }
}
