using UnityEngine;
using System.Collections;
using System;

public class EnemyScript :  PlayerScript
{  
    public YouScript player;
	public float senRadius;	// sensitive radius

	NavMeshAgent2D nav;

	override protected void Start()
	{
		nav = GetComponent<NavMeshAgent2D>();
		nav.destination = player.pos2();
		// put base' same function in the last!
		base.Start();
	}
	// Update is called once per frame
	override protected void Update()
    {
        nav.destination = player.pos2();
        adjustFace();
		// call base at last
		base.Update();
    }
	void adjustFace()
	{		
		float theta = Mathf.Atan2(nav.velocity.y, nav.velocity.x);
		transform.rotation = Quaternion.Euler(0, 0, theta * 180.0f / Mathf.PI);
	}

	// AI part: decide 1.flee? 2.chase? 3.roam!
	char decide()
	{
		return 'r';
	}
	void flee()
	{

	}
	void chase()
	{

	}
	void roam()
	{

	}
	// end of AI part

	override public Vector2 vel2()
	{
		return new Vector2(nav.velocity.x, nav.velocity.y);
	}
	public override Vector2 pos2()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	// we do collision detecting only in enemy, since enemy eats enemy and You
	void OnCollisionEnter2D(Collision2D coll)
	{
		PlayerScript other = coll.gameObject.GetComponent<PlayerScript>();
		if (other == null)
			return;
		Debug.Log("Eat! size:[" + radius + "] VS size:[" + other.rad1());
		// 1. the larger one: grow
		// 2.the smaller one: be destroyed
		if (other.rad1() > radius)
		{
			Debug.Log("Eat! will destroy:" + radius);
			other.eat(radius);
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("Eat! will destroy:" + other.rad1());
			Destroy(other.gameObject);
			eat(other.rad1());
		}
	}
}