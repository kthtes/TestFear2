using UnityEngine;
using System.Collections;
using System;

public class EnemyScript :  PlayerScript
{  
    public YouScript player;
	NavMeshAgent2D nav;

	void Start()
	{
		setSizeLevel(sizeLevel);
		nav= GetComponent<NavMeshAgent2D>();
		nav.destination = player.pos2();
	}
	// Update is called once per frame
	void Update()
    {
        nav.destination = player.pos2();
        adjustFace();
    }
	void adjustFace()
	{		
		float theta = Mathf.Atan2(nav.velocity.y, nav.velocity.x);
		transform.rotation = Quaternion.Euler(0, 0, theta * 180.0f / Mathf.PI);
	}

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
		Debug.Log("Eat! size:[" + sizeLevel + "] VS size:[" + other.sizeLevel);
		// 1. the larger one: grow
		if (other.sizeLevel > sizeLevel)
		{
			Debug.Log("Eat! will destroy:" + sizeLevel);
			other.grow();
			Destroy(gameObject, 0.5f);
		}
		else
		{
			Debug.Log("Eat! will destroy:" + other.sizeLevel);
			Destroy(other.gameObject, 0.5f);
			grow();
		}
		//2.the smaller one: destroy
	}
}