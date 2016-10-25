using UnityEngine;
using System.Collections;

public class YouScript : PlayerScript
{
	public float moveForce = 4.0f;

	// forces in 4 directions 
	float[] curForces = { 0, 0, 0, 0 };

	// move to directions! d=[0,1,2,3]=[up,right,down,left]
	void onDirection(int d, bool move = true)
	{
		curForces[d] = move ? moveForce : 0;
		ConstantForce2D f = GetComponent<ConstantForce2D>();
		f.force = new Vector2(curForces[1] - curForces[3], curForces[0] - curForces[2]);
	}

	// Update is called once per frame
	void Update()
	{
		// Keyboard --> change forces
		onDirection(0, Input.GetKey(KeyCode.W));
		onDirection(1, Input.GetKey(KeyCode.D));
		onDirection(2, Input.GetKey(KeyCode.S));
		onDirection(3, Input.GetKey(KeyCode.A));

		// check speed limit
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = Toolbox.Instance.confineVelocity(moveSpeed, rb.velocity);

		// for debug: G=Grow, R=Reduce
		if (Input.GetKeyUp(KeyCode.G))
			grow();
		else if (Input.GetKeyUp(KeyCode.R))
			reduce();
		else if (Input.GetKeyUp(KeyCode.B))
			bleed();
	}

	override public void setSizeLevel(int level)
	{
		base.setSizeLevel(level);
		moveForce = 4.0f * Mathf.Pow(1.4f, level);
	}
}
