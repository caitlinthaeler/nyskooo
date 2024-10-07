using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{

    public Button easyButton;

    public Button difficultButton;

    public Button controlsScreenButton;

    public Button backButton;

    private bool canContinue;

    public GameObject menuObject;

    public GameObject controlsBackground;

    public SceneManagerScript sceneManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to SceneManagerScript
        sceneManagerScript = FindObjectOfType<SceneManagerScript>();
        menuObject.SetActive(true);
        controlsBackground.SetActive(false);
        AddListeners();
        canContinue = false;
        
    }

    private void AddListeners()
    {
        easyButton.onClick.AddListener(OnEasyButtonClick);
        difficultButton.onClick.AddListener(OnDifficultButtonClick);
        controlsScreenButton.onClick.AddListener(onContinueButtonClick);
        backButton.onClick.AddListener(onBackButtonClick);
    }

    private void FixedUpdate()
    {
        if (canContinue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onContinueButtonClick();
                canContinue = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }
    private void OnEasyButtonClick()
    {
        Debug.Log("Easy Button Clicked");
        PlayerPrefs.SetInt("level", 0);
        ActivateControlScreen();
        PlayerPrefs.Save();
        
    }

    private void OnDifficultButtonClick()
    {
        Debug.Log("Difficult Button Clicked");
        PlayerPrefs.SetInt("level", 1);
        ActivateControlScreen();
        PlayerPrefs.Save();
    }

    private void onContinueButtonClick()
    {
        sceneManagerScript.LoadGame();
    }

    private void onBackButtonClick()
    {
        ReturnToMenu();
    }

    private void ActivateControlScreen()
    {
        menuObject.SetActive(false);
        controlsBackground.SetActive(true);
        canContinue = true;
        
    }

    private void ReturnToMenu()
    {
        controlsBackground.SetActive(false);
        menuObject.SetActive(true);
        canContinue = false;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
