using UnityEngine;
using System.Collections;

public class YouScript : PlayerScript
{
	bool closeEnough(){
		float lim = Mathf.Max (0.3f*rad1(),0.2f);
		return Vector2.Distance (nav.destination, pos2 ()) < lim;
	}

	// Update is called once per frame
	override protected void Update()
	{
		nav.autoBraking = true;
		nav.stoppingDistance = rad1 () * 0.5f;
		// mouse click left
		if (Input.GetMouseButton (0)) {
			Debug.Log ("Pressed !! nav is "+nav.destination.x);
			Vector3 dest = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 dest2 = new Vector2 (dest.x, dest.y);
			nav.SetDestination (dest2);
			nav.Resume ();
		}
		if (closeEnough ())
			nav.Stop ();

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
}
