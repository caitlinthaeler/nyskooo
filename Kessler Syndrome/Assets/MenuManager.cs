using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button startButtonEasy;
    public Button startButtonDifficult;
    public GameObject menuScreen;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("is active"+startButtonEasy.IsActive());
        SceneManager.UnloadSceneAsync("SampleScene");
        startButtonEasy.onClick.AddListener(StartGameEasy);
        startButtonDifficult.onClick.AddListener(StartGameDifficult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameEasy()
    {
        SceneManager.LoadScene("SampleScene");

        //set level
        menuScreen.SetActive(false);
        PlayerPrefs.SetInt("level", 0);
    }

    public void StartGameDifficult()
    {
        SceneManager.LoadScene("SampleScene");

        //set level
        menuScreen.SetActive(false);
        PlayerPrefs.SetInt("level", 1);
    }
}
