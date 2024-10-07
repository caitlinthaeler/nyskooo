using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;

public class SceneManagerScript : MonoBehaviour
{
    public string mainSceneName = "GameScene";
    public string menuSceneName = "MenuScene";

    public void LoadGame()
    {

        // Load the game scene asynchronously
        SceneManager.LoadSceneAsync(mainSceneName, LoadSceneMode.Single);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnGameSceneLoaded;
        SceneManager.LoadScene(mainSceneName);
    }

    // Callback when the new scene is loaded
    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the menu scene is currently loaded before attempting to unload it
        Scene menuScene = SceneManager.GetSceneByName(menuSceneName);
        if (menuScene.isLoaded)
        {
            // Unload the menu scene
            SceneManager.UnloadSceneAsync(menuSceneName);
        }

        // Unsubscribe from the event to avoid memory leaks
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }
}
