using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class PlayerScript : MonoBehaviour
{  // You's attributes
	public float eatAbsorbFactor = 0.5f;
	public float speedGrowFactor = 0.5f;
	public float scale0 = 0.1f;
	public float moveSpeed0 = 1.5f;		// the move speed of radius==0.1

	protected float scale;				// go with scale
	protected float moveSpeed;          // go with scale

	protected float senRadius;			// changed with radius, now it is 3.0f*radius

	// property functions
	abstract public Vector2 vel2();
	abstract public Vector2 pos2();
	public float scale1(){
		return scale;
	}
	public float rad1() {
		if (GetComponent<BoxCollider2D> () != null) {
			return GetComponent<BoxCollider2D> ().bounds.size.x;
		} else if (GetComponent<CircleCollider2D> () != null) {
			return GetComponent<CircleCollider2D> ().bounds.size.x;
		} else
			Debug.LogError ("*** PlayerScript: can NOT find a Box/Circle Collider!!");
		return 0;
	}

	// Use this for initialization
	virtual protected void Start()
	{
		scale = transform.localScale.x;
		applyRadiusChange();
	}
	virtual protected void Update()
	{
		Dictionary<string, float> dict = new Dictionary<string, float>();
		dict.Add("s", scale);
		dict.Add("x", pos2().x);
		dict.Add("y", pos2().y);
		dict.Add ("r", rad1 ());
		dict.Add("sen", senRadius);
		Toolbox.Instance.playerData[name] = dict;
	}
	virtual protected void applyRadiusChange()
	{
		transform.localScale = new Vector3(scale, scale, transform.localScale.z);
		moveSpeed = moveSpeed0 * (scale / scale0) * speedGrowFactor;
		senRadius = 3.0f * rad1 ();
	}

	// action functions
	public void eat(float otherRadius)
	{
		float destArea = Mathf.PI * scale * scale + eatAbsorbFactor * Mathf.PI * otherRadius * otherRadius;
		scale = Mathf.Sqrt(destArea / Mathf.PI);
		applyRadiusChange();
	}
    public void grow(float by=0.1f)
    {
		scale += by;
		scale = Mathf.Min(10.0f, scale);
		applyRadiusChange();
    }
    public void reduce(float by=0.1f)
    {
		scale -= by;
		scale = Mathf.Max(0.01f, scale);
		applyRadiusChange();
    }
    public void bleed()
    {

    }
}
