using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class PlayerScript : MonoBehaviour
{  
	protected NavMeshAgent2D nav;
	// You's attributes
	public float scale0 = 0.1f;
	public float eatAbsorbFactor = 0.5f;
	public float speedGrowFactor = 0.1f;
	public float accelGrowFactor = -0.1f;
	public float accel0 = 8.0f;
	public float speed0 = 2.0f;		// the move speed of radius==0.1

	protected float scale;				// go with scale

	protected float senRadius;			// changed with radius, now it is 3.0f*radius

	// property functions
	public Vector2 vel2() {return nav.velocity;}
	public Vector2 pos2() {
		return new Vector2 (transform.position.x, transform.position.y);
	}
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
		// get nav
		nav = GetComponent<NavMeshAgent2D> ();
		nav.autoBraking = false;
		nav.stoppingDistance = 0.5f;
		// set scale
		scale = transform.localScale.x;
		// update
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
		nav.speed = speed0 + speed0 * (scale / scale0) * speedGrowFactor;
		nav.acceleration =  accel0 + accel0 * (scale / scale0) * accelGrowFactor;
		senRadius = 3.0f * rad1 ();
	}

	// action functions
	public void adjustFace(){
		if (Toolbox.Instance.velToSpd (nav.velocity) < 0.1f)
			return;
		float theta = Mathf.Atan2(nav.velocity.y, nav.velocity.x);
		transform.rotation = Quaternion.Euler(0, 0, theta * 180.0f / Mathf.PI);
	}
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
