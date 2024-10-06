using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpaceJunkLevelsData", menuName = "ScriptableObjects/SpaceJunkLevelsData", order = 4)]
public class SpaceJunkLevelsScriptableObject : ScriptableObject
{
    // List of level descriptions with a sliding range for levels
    public List<LevelDescription> descriptionsByLevel = new List<LevelDescription>();
}


[System.Serializable]
public class LevelDescription
{
    [Range(0.0f, 1.0f)] // Set the range for the level
    public float level; // Represents the level of space junk

    public string description; // Description of the level

    public LevelDescription(float level, string description)
    {
        this.level = level;
        this.description = description;
    }
}
