using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    /* Update player's remaining lives */
    void Update()
    {
        livesText.text = PlayerStats.Lives + " LIVES";
    }
}
