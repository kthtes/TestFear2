using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";
	public Dictionary<string, Dictionary<string, float>> playerData=new Dictionary<string, Dictionary<string, float>>();

    void Awake()
    {
        // Your initialization code here
    }

	public float velToSpd(Vector2 vel)
	{
		return Mathf.Sqrt(vel.x * vel.x + vel.y * vel.y);
	}

    public Vector2 confineVelocity(float speed, Vector2 vel)
    {
        float curSpeed = Mathf.Sqrt(vel.x * vel.x + vel.y * vel.y);
        if (curSpeed <= speed)
            return vel;
        float theta = Mathf.Atan2(vel.y, vel.x);
        return new Vector2(speed * Mathf.Cos(theta), speed * Mathf.Sin(theta));
    }

	public float confine(float min, float x, float max)
	{
		return Mathf.Max(min, Mathf.Min(x, max));
	}
	public Dictionary<string,float> playerDataAt(int index)
	{
		string[] keys = new string[playerData.Keys.Count];
		playerData.Keys.CopyTo(keys,0);
		Array.Sort(keys);
		return playerData[keys[index]];
	}
}

