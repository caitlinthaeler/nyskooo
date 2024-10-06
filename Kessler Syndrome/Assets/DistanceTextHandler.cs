using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceTextHandler : MonoBehaviour
{
    private int totalDistance;

    private int endX;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        InitText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitText()
    {
        // put in scriptable object?
        totalDistance = 30000;
    }

    public void UpdateDistanceText(float percentageDistanceFromGoal)
    {
        Debug.Log("percentageDistanceFromGoal: " + percentageDistanceFromGoal);
        Debug.Log("totalDistance: " + totalDistance);
        int currentDistanceKM = (int)(percentageDistanceFromGoal * totalDistance);
        Debug.Log("Distance To Earth: " + currentDistanceKM + "km");
        textMeshPro.SetText("Distance To Earth: "+currentDistanceKM+"km");
        
    }
}
