using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHandler : MonoBehaviour
{

    public EarthScriptableObject earthData;

    private Player player;

    private Transform t;

    private float initX;

    private float endX;

    private float initScale;

    private float endScale;

    private float xDifference;

    private float scaleDifference;

    // Start is called before the first frame update
    void Start()
    {
        InitEarthData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitEarthData()
    {
        initX = earthData.initX;
        endX = earthData.finalX;
        initScale = earthData.initScale;
        endScale = earthData.finalScale;
        xDifference = initX - endX;
        scaleDifference = endScale - initScale;
        t = gameObject.transform;
    }


    public void SetEarthPosition(float percentageDistanceFromGoal)
    {
        //Debug.Log("percentageDistancefromGoal: " + percentageDistanceFromGoal);
        float currentXDifference = percentageDistanceFromGoal * xDifference;
        float currentScaleDifference = percentageDistanceFromGoal * scaleDifference;
        float x = endX + currentXDifference;
        //Debug.Log("endX("+endX+") + currentXDifference("+currentXDifference+") = x("+x);
        float s = endScale - currentScaleDifference;

        Transform t = gameObject.transform;

        t.localPosition= new Vector3(x, t.localPosition.y);
        t.localScale = new Vector3(s, s, s);
    }
}
