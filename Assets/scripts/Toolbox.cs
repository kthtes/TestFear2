using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    void Awake()
    {
        // Your initialization code here
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
    // (optional) allow runtime registration of global objects
    //static public T RegisterComponent<T>() where T : Component
    //{
    //    return Instance.GetOrAddComponent<T>();
    //}
}

