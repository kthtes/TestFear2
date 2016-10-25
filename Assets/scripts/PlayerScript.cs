﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{  // You's attributes
    public int sizeLevel = 0;
	public float moveSpeed = 1.5f;

	// Use this for initialization
	void Start ()
    {
        // 1. reset its size
        setSizeLevel(sizeLevel);
	}

    // public functions
    public Vector2 position()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
    virtual public void setSizeLevel(int level)
    {
        // validate level
        if (level < 0 || level >= 9)
        {
            Debug.LogWarning("PlayerScript.setSizeLevel(): sizeLevel must >=0 and < 9!!!");
            return;
        }
        // set size level
        sizeLevel = level;
        float size = 0.1f * Mathf.Pow(1.5f, level);
        transform.localScale = new Vector3(size, size, 1);
        // refresh force/speed
        moveSpeed = 1.5f * Mathf.Pow(1.7f, level);
    }
    public void grow()
    {
        setSizeLevel(sizeLevel + 1);
    }
    public void reduce()
    {
        setSizeLevel(sizeLevel - 1);
    }
    public void bleed()
    {

    }
}
