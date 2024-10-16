using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 0.5f;
    public void LoadScence()
    {
        int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex");
        Scorekeeper.instance.ResetScore();
        SceneManager.LoadScene(previousSceneIndex);

    }
    public void LoadMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
    public void LoadSceneNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("PreviousSceneIndex", currentSceneIndex);
        SceneManager.LoadScene("NextLevel");
    }
    public void LoadNextLevel()
    {
        StartCoroutine(NextLevel());

    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(sceneLoadDelay); 
        int previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex");
        int nextScenceIndex = previousSceneIndex + 1;
        if (nextScenceIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("GameOver");
        }
        SceneManager.LoadScene(nextScenceIndex);
        // PlayerPrefs.DeleteKey("PreviousSceneIndex");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }

}
