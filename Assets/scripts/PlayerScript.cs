using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class PlayerScript : MonoBehaviour
{  // You's attributes
	public float eatAbsorbFactor = 0.5f;
	public float speedGrowFactor = 0.5f;
	public float radius0 = 0.1f;
	public float moveSpeed0 = 1.5f;	// the move speed of radius==0.1

	protected float radius;			// go with scale
	protected float moveSpeed;			// go with scale

	// property functions
	abstract public Vector2 vel2();
	abstract public Vector2 pos2();
	public float rad1() { return radius; }

	// Use this for initialization
	virtual protected void Start()
	{
		radius = transform.localScale.x;
		applyRadiusChange();
	}
	virtual protected void Update()
	{
		Dictionary<string, float> dict = new Dictionary<string, float>();
		dict.Add("r", radius);
		dict.Add("x", pos2().x);
		dict.Add("y", pos2().y);
		Toolbox.Instance.playerData[name] = dict;
	}
	virtual protected void applyRadiusChange()
	{
		transform.localScale = new Vector3(radius, radius, transform.localScale.z);
		moveSpeed = moveSpeed0 * (radius / radius0) * speedGrowFactor;
	}

	// action functions
	public void eat(float otherRadius)
	{
		float destArea = Mathf.PI * radius * radius + eatAbsorbFactor * Mathf.PI * otherRadius * otherRadius;
		radius = Mathf.Sqrt(destArea / Mathf.PI);
		applyRadiusChange();
	}
    public void grow(float by=0.1f)
    {
		radius += by;
		radius = Mathf.Min(10.0f, radius);
		applyRadiusChange();
    }
    public void reduce(float by=0.1f)
    {
		radius -= by;
		radius = Mathf.Max(0.01f, radius);
		applyRadiusChange();
    }
    public void bleed()
    {

    }
}
