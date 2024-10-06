using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public enum LevelEnum
    {
        LevelEasy,
        LevelDifficult
    }

    public List<LevelScriptableObject> levelObjects = new List<LevelScriptableObject>();

    // This will be the dropdown in the inspector
    public LevelEnum selectedLevel;

    [System.NonSerialized]
    public LevelScriptableObject levelData;

    // Start is called before the first frame update
    void Start()
    {
        if (selectedLevel == LevelEnum.LevelEasy)
        {
            levelData = levelObjects[0];
        } else
        {
            levelData = levelObjects[1];
        }
    }
}
