using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField]
    private GameObject filling;

    private RectTransform fillingRect;

    private float fillingWidth;

    private float fillingHeight;

    [SerializeField]
    private GameObject healthTextObject;

    private TextMeshProUGUI text;

    private int health;

    public int Health { get => health; set => UpdateFilling(value);  }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHealth(LevelScriptableObject data)
    {
        health = data.initHealth;
        fillingRect = filling.GetComponent<RectTransform>();
        fillingWidth = fillingRect.sizeDelta.x;
        fillingHeight = fillingRect.sizeDelta.y;
        text = healthTextObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(text.text);
        UpdateFilling(health);

    }


    private void UpdateFilling(int value)
    {
        Debug.Log("updating health to "+value);
        //ensure that the value doesn't exceed the min and max health
        value = Mathf.Clamp(value, 0, health);
        float percentage = (float)value / health;
        fillingRect.sizeDelta = new Vector2(fillingWidth, percentage * fillingHeight);
        text.SetText(value + "/" + health);
        
        //text.text = value + "/" + health;
    }
}
