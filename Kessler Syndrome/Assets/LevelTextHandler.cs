using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTextHandler : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    public string LevelText { get => textMeshPro.text; set => UpdateLevelText(value); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void UpdateLevelText(string text)
    {
        textMeshPro.SetText(text);
    }
}
