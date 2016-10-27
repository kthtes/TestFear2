using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript :  PlayerScript
{
	// Behavior 
	public bool eater=false;
	public bool fleer=false;
	public bool roamer=true;

	// current mode: [idle, eat, flee, roam]
	public string mode="idle";

	// roam center pos
	Vector2 roamCenter=Vector2.zero;

	Dictionary <string,Dictionary<string,float>> playerData;

	override protected void Start()
	{
		// put base' same function in the last!
		base.Start();
		// set playerData
		playerData=Toolbox.Instance.playerData;
	}
	// Update is called once per frame
	override protected void Update()
    {
		// call base at last
		base.Update();
		// TODO: set speed if roaming

		// make a move
		decide();
		adjustFace();
    }

	// AI part: decide 1.flee? 2.chase? 3.roam!
	void decide()
	{
		// 1. flee if necessary
		Dictionary<string, float> dan=dangeOne();
		if (fleer && dan!=null) {
			fleeFrom (dan["x"],dan["y"]);
			return;
		}
		// 2. chase if necessary
		Dictionary<string,float> eat=edibleOne();
		if (eater && eat!=null) {
			chaseFor (eat["x"],eat["y"]);
			return;
		}
		// 3. roam
		if(roamer)
			roam();
	}
	void chasePlayer(){
		mode = "chase";
		Vector2 pos = new Vector2 (playerData ["You"] ["x"], playerData ["You"] ["y"]);
		nav.SetDestination (pos);
		// set auto repath
		nav.agent.autoRepath = true;
	}
	void fleeFrom(float x, float y)
	{
		mode = "flee";
		// TODO: give it a random factor
		Vector2 dir=new Vector2(pos2().x-x, pos2().y-y);
		Vector2 newDir = Toolbox.Instance.vectorRotate (dir, pos2 (), Random.Range (-0.2f * Mathf.PI, 0.2f * Mathf.PI));
		nav.SetDestination (1.5f*(newDir - dir));
		// give the nav a "autoBrake"
		nav.autoBraking=false;
		nav.agent.autoRepath = false;
	}
	void chaseFor(float x, float y)
	{
		mode = "chase";
		nav.SetDestination (new Vector2 (x, y));
		// do not autoBrake
		nav.autoBraking=false;
		nav.agent.autoRepath = false;
	}
	void roam()
	{
		if (mode != "roam") {
			mode = "roam";
			roamCenter = pos2 ();
			nav.SetDestination (roamGenPos (roamCenter));
			nav.autoBraking = true;
			return;
		}
		if (nav.agent.remainingDistance < 0.1f) {			
			nav.SetDestination (roamGenPos (roamCenter));
			nav.autoBraking = true;
			return;
		}
	}
	Vector2 roamGenPos(Vector2 pos){
		float dx = Random.Range (0,1.0f) < 0.5f ? Random.Range (-1.0f, -0.3f) : Random.Range (0.3f, 1.0f);
		float dy = Random.Range (0,1.0f) < 0.5f ? Random.Range (-1.0f, -0.3f) : Random.Range (0.3f, 1.0f);

		return new Vector2 (pos.x + dx, pos.y + dy);
	}
	bool roamCloseToDest(){
		return Vector2.Distance (pos2 (), nav.destination) < 0.5f;
	}

	Dictionary<string,float> dangeOne(){
		if (playerData.Count == 0)
			return null;
		foreach (string oneName in playerData.Keys) {
			if (name == oneName)
				continue;
			float r1 = playerData [oneName] ["r"];
			float sen1 = playerData [oneName] ["sen"];
			float dist = Toolbox.Instance.playerDist (oneName, name);
			if (r1 > rad1() && dist < sen1)
				return playerData [oneName];
		}
		return null;
	}

	Dictionary<string,float> edibleOne(){
		if (playerData.Count == 0)
			return null;
		// TODO: use the eat list to determine that
//		List<string> eatList = new List<string> ();
		//
		foreach (string oneName in playerData.Keys){
			if (name == oneName)
				continue;
			float sPercent = playerData [oneName] ["s"] / scale;
			float distPercent = Toolbox.Instance.playerDist (oneName, name) / senRadius;
			if (distPercent > 1.0f)
				continue;
			if (sPercent > 0.3f && sPercent < 0.9f)
				return playerData [oneName];
		}
		return null;
	}
	// end of AI part

	// we do collision detecting only in enemy, since enemy eats enemy and You
	void OnCollisionEnter2D(Collision2D coll)
	{
		PlayerScript other = coll.gameObject.GetComponent<PlayerScript>();
		if (other == null)
			return;
		Debug.Log("Eat! size:[" + scale + "] VS size:[" + other.scale1());
		// 1. the larger one: grow
		// 2.the smaller one: be destroyed
		if (other.scale1() > scale)
		{
			Debug.Log("Eat! will destroy:" + scale);
			other.eat(scale);
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("Eat! will destroy:" + other.scale1());
			Destroy(other.gameObject);
			eat(other.scale1());
		}
	}
}