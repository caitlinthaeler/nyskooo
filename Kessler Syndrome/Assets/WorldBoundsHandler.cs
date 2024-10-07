using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsHandler : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    private Bounds worldBounds;

    public Bounds WorldBounds { get => worldBounds; }

    public void InitBounds()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        worldBounds = boxCollider.bounds;
        Debug.Log("world bounds for world bounds handler: " + worldBounds.min + ", " + worldBounds.max);

    }

    

    
}
