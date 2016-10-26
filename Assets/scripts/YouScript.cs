using UnityEngine;
using System.Collections;

public class YouScript : PlayerScript
{
	public float moveForce0 = 4.0f;
	public float moveForceGrowFactor = 0.5f;

	float moveForce;

	protected override void Start()
	{
		// TODO - own start()

		// put base' function in the last line!
		base.Start();
	}
	override protected void applyRadiusChange()
	{
		// moveForce
		moveForce = (transform.localScale.x / radius0) * moveForce0 * moveForceGrowFactor;
		// call base at last
		base.applyRadiusChange();
	}

	// Update is called once per frame
	override protected void Update()
	{
		// Keyboard --> change forces
		onDirection(0, Input.GetKey(KeyCode.W));
		onDirection(1, Input.GetKey(KeyCode.D));
		onDirection(2, Input.GetKey(KeyCode.S));
		onDirection(3, Input.GetKey(KeyCode.A));

		// check speed limit
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = Toolbox.Instance.confineVelocity(moveSpeed, rb.velocity);

		// adjust rotation
		adjustFace();

		// for debug: G=Grow, R=Reduce
		if (Input.GetKeyUp(KeyCode.G))
			grow();
		else if (Input.GetKeyUp(KeyCode.R))
			reduce();
		else if (Input.GetKeyUp(KeyCode.B))
			bleed();
		// call base at last
		base.Update();
	}

	void adjustFace()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		// don't do this when vel is very small
		if (Toolbox.Instance.velToSpd(rb.velocity) < 0.5f)
			return;
		float theta = Mathf.Atan2(vel2().y, vel2().x);
		transform.rotation = Quaternion.Euler(0, 0, theta * 180.0f / Mathf.PI);
	}
	// move to directions! d=[0,1,2,3]=[up,right,down,left]
	// forces in 4 directions 
	float[] curForces = { 0, 0, 0, 0 };
	void onDirection(int d, bool move = true)
	{
		curForces[d] = move ? moveForce : 0;
		ConstantForce2D f = GetComponent<ConstantForce2D>();
		f.force = new Vector2(curForces[1] - curForces[3], curForces[0] - curForces[2]);
	}

	override public Vector2 pos2()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}
	override public Vector2 vel2()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		return new Vector2(rb.velocity.x, rb.velocity.y);
	}
}
