using UnityEngine;
using System.Collections;

abstract public class PlayerScript : MonoBehaviour
{  // You's attributes
    public int sizeLevel = 0;
	public float sizeGrowFactor = 1.5f;
	public float speedGrowFactor = 1.7f;
	public float moveSpeed = 1.5f;
	public float someFloat;

	// Use this for initialization
	void Start ()
    {
        // 1. reset its size
        setSizeLevel(sizeLevel);
	}
	// abstract functions
	abstract public Vector2 vel2();
	abstract public Vector2 pos2();
    // public functions
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
        float size = 0.1f * Mathf.Pow(sizeGrowFactor, level);
        transform.localScale = new Vector3(size, size, 1);
        // refresh force/speed
        moveSpeed = moveSpeed * Mathf.Pow(speedGrowFactor, level);
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
