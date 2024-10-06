using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEarthData", menuName ="ScriptableObjects/EarthData", order = 3)]
public class EarthScriptableObject : ScriptableObject
{
    //these values should not be changed outside of the object
    public float initScale = 1;
    public float initX = 11;
    public float initY = 0;


    public float finalScale = 5;
    public float finalX = 8;
}
