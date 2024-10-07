using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string mainSceneName = "GameScene";
    public string gameOverSceneName = "GameOverScene";
    // Start is called before the first frame update

    public void EndGame()
    {
        LoadGameOver();
    }

    public void LoadGameOver()
    {

        // Load the game over scene asynchronously
        SceneManager.LoadSceneAsync(gameOverSceneName, LoadSceneMode.Single);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnGameOverSceneLoaded;
        SceneManager.LoadScene(gameOverSceneName);
    }

    // Callback when the new scene is loaded
    private void OnGameOverSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the game scene is currently loaded before attempting to unload it
        Scene mainScene = SceneManager.GetSceneByName(mainSceneName);
        if (mainScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(mainSceneName);
        }

        // Unsubscribe from the event to avoid memory leaks
        SceneManager.sceneLoaded -= OnGameOverSceneLoaded;
    }
}
