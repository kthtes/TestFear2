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
	public string playerNameAt(int index){
		string[] keys = new string[playerData.Keys.Count];
		playerData.Keys.CopyTo(keys,0);
		Array.Sort(keys);
		return keys [index];
	}
	public Dictionary<string,float> playerDataAt(int index)
	{
		return playerData[playerNameAt(index)];
	}
	public Dictionary<string,float> playerDataNamed(string name){
		return playerData [name];
	}
	public float playerDist(string n1, string n2){
		Vector2 pos1 = new Vector2 (playerData [n1] ["x"], playerData [n1] ["y"]);
		Vector2 pos2 = new Vector2 (playerData [n2] ["x"], playerData [n2] ["y"]);
		return Vector2.Distance (pos1, pos2);
	}
	public Vector2 vectorRotate(Vector2 p1, Vector2 center, float angle)  
	{  
		float x1 = (p1.x - center.x) * Mathf.Cos(angle) + (p1.y - center.y) * Mathf.Sin(angle) + center.x;  
		float y1 = -(p1.x - center.x) * Mathf.Sin(angle) + (p1.y - center.y) * Mathf.Cos(angle) + center.y;  
		return new Vector2 (x1, y1);
	}
}

