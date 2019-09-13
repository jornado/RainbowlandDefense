using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public GameObject aboutUi;
    public GameObject mainUi;

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator PlayAsync()
    {
        // TODO fade into scene
        AsyncOperation AO = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f)
        {
            yield return null;
        }

        //Fade the loading screen out here

        AO.allowSceneActivation = true;
//        SceneManager.LoadScene(levelToLoad);
    }

    public void About()
    {
        // Enable about screen
        mainUi.SetActive(false);
        aboutUi.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
