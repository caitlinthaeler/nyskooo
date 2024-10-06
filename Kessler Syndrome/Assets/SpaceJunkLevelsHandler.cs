using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceJunkLevelsHandler : MonoBehaviour
{

    public SpaceJunkLevelsScriptableObject descriptionData;

    public List<LevelDescription> descriptionsByLevel;

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
        descriptionsByLevel = descriptionData.descriptionsByLevel;
    }

    public void UpdateSpaceJunkLevelsDescription(float value)
    {
        string description = "Clear";
        for (int i = 0; i < descriptionsByLevel.Count; i++)
        {
            LevelDescription ld = descriptionsByLevel[i];
            if (value < ld.level)
            {
                break;
            } else
            {
                description = ld.description;
            }
        }
        textMeshPro.SetText("space junk levels: "+description);
    }
}
