using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public Button testButton; // Reference to the button

    void Start()
    {
        // Add listener for button click
        testButton.onClick.AddListener(OnButtonClick);

    }


    void OnButtonClick()
    {
        Debug.Log("Button Clicked!"); // Log message on button click
    }
}

