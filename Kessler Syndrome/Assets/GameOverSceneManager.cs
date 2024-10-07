using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    public string gameOverSceneName = "GameOverScene";
    public string menuSceneName = "MenuScene";

    // Start is called before the first frame update

    public void LoadMainMenu()
    {

        // Load the menuscene asynchronously
        SceneManager.LoadSceneAsync(menuSceneName, LoadSceneMode.Single);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnMenuSceneLoaded;
        SceneManager.LoadScene(menuSceneName);
    }

    // Callback when the new scene is loaded
    private void OnMenuSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the menu scene is currently loaded before attempting to unload it
        Scene gameOverScene = SceneManager.GetSceneByName(gameOverSceneName);
        if (gameOverScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameOverSceneName);
        }

        // Unsubscribe from the event to avoid memory leaks
        SceneManager.sceneLoaded -= OnMenuSceneLoaded;
    }
}
