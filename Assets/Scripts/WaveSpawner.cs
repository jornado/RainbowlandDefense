using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    public float timeBetweenEnemies = 0.5f;

    public TextMeshProUGUI waveCountdownText;

    private float countdown = 2f;
    private int waveIndex;

    /* Spawn a wave when countdown reaches zero */
    private void Update()
    {
        // Spawn enemies each wave, when timer reaches zero
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // Reduce timer by one sec
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
        // Change text element to timer value
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    /* Spawn a new wave */
    IEnumerator SpawnWave()
    {
        // Spawn as many enemies as our current wave number, i.e.:
        // Wave 3 = Spawn 3 enemies
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            // Wait between spawning enemies
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
  
    }

    /* Spawn a new enemy */
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }


}
