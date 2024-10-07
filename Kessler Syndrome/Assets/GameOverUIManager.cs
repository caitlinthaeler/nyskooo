using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameOverUIManager : MonoBehaviour
{
    public Button tryAgainButton;

    public GameOverSceneManager gameOverSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to SceneManagerScript
        gameOverSceneManager = FindObjectOfType<GameOverSceneManager>();
        AddListeners();

    }

    private void AddListeners()
    {
        tryAgainButton.onClick.AddListener(onTryAgainButtonClick);
    }

    private void onTryAgainButtonClick()
    {
        gameOverSceneManager.LoadMainMenu();
    }
}
