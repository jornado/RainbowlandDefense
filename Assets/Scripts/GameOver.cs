using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI wavesSurvivedText;
    public string menuScreen = "MainMenu";

    void OnEnable()
    {
        // set the number of waves survived
        wavesSurvivedText.text = PlayerStats.WavesSurvived.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go to menu");
        SceneManager.LoadScene(menuScreen);
    }
}
