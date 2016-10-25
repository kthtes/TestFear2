﻿using UnityEngine;
using System.Collections;

public class EnemyScript : PlayerScript
{  
    public YouScript player;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = player.position();
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        float dx = pos.x - myPos.x;
        float dy = pos.y - myPos.y;
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dx < 0 ? -moveSpeed : moveSpeed, dy < 0 ? -moveSpeed : moveSpeed);
        rb.velocity = Toolbox.Instance.confineVelocity(moveSpeed, rb.velocity);
    }
}