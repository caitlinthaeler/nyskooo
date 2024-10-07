using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentManager : MonoBehaviour
{
    private void Awake()
    {
        // Check if an instance of this GameObject already exists
        if (FindObjectsOfType<PersistentManager>().Length > 1)
        {
            Destroy(gameObject); // Destroy this instance if one already exists
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Keep this instance alive when switching scenes

            // Check for an existing EventSystem
            if (FindObjectOfType<EventSystem>() == null)
            {
                // Add an EventSystem component if it doesn't exist
                gameObject.AddComponent<EventSystem>();
                gameObject.AddComponent<StandaloneInputModule>(); // Add input module for UI interactions
            }
        }
    }
}