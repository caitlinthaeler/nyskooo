using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunkHandler : MonoBehaviour
{

    public SpaceJunkData initData;

    private Rigidbody2D rb;

    private int playerDamage;

    public int PlayerDamage { get => playerDamage; }

    // Start is called before the first frame update
    void Start()
    {
        InitSpaceJunk(initData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitSpaceJunk(SpaceJunkData data)
    {
        playerDamage = data.playerDamage;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.mass = data.mass;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }
}
