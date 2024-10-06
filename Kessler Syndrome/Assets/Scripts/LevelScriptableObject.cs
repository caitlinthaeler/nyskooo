using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
public class LevelScriptableObject : ScriptableObject
{
    //these values should not be changed outside of the object
    public string levelName;
    public int level;
    public int initHealth;
    public float endSpawnOffset;

    [System.NonSerialized]
    public float maxDensity = 1;

    [Space(10), Range(0.0f, 1f)]
    public float highOrbitObjectDensity;
    public GameObject[] highOrbitObjects;

    [Space(10), Range(0.0f, 1f)]
    public float mediumOrbitObjectDensity;
    public GameObject[] mediumOrbitObjects;

    [Space(10), Range(0.0f, 1f)]
    public float lowOrbitObjectDensity;
    public GameObject[] lowOrbitObjects;
}
