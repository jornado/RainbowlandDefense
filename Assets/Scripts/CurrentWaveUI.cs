using UnityEngine;
using TMPro;

public class CurrentWaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;

    /* Update player's remaining money */
    void Update()
    {
        waveText.text = "WAVE " + WaveSpawner.instance.GetCurrentWave().ToString();
    }
}
