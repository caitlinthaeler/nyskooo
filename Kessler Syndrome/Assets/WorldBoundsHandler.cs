using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsHandler : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    private Bounds worldBounds;

    public Bounds WorldBounds { get => worldBounds; }

    private void Start()
    {
        InitBounds();
    }
    private void InitBounds()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        worldBounds = boxCollider.bounds;
        
    }

    

    
}
