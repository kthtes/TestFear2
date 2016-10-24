using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{  // You's attributes
    public int sizeLevel = 0;
    public float moveForce = 4.0f;
    public float moveSpeed = 1.5f;
    ////  adjust the "step" of the size here.
    //float[] sizeTable = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
    //float[] forceTable = {3.0f, 9.0f, 27.0f, 81.0f, 243.0f };
    //float[] speedTable = { 1.5f, 2.0f, 2.5f, 3.0f, 3.5f };

    // forces in 4 directions 
    float[] curForces;

	// Use this for initialization
	void Start ()
    {
        // 1. reset current forces within 4 directions
        curForces = new float[4] { 0, 0, 0, 0 };
        // 2. reset its size
        setSizeLevel(sizeLevel);
	}

    // move to directions! d=[0,1,2,3]=[up,right,down,left]
    void onDirection(int d, bool move=true)
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

    // public functions
    public Vector2 position()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
    public void setSizeLevel(int level)
    {
        // validate level
        if (level < 0 || level >= 9)
        {
            Debug.LogWarning("PlayerScript.setSizeLevel(): sizeLevel must >=0 and < 9!!!");
            return;
        }
        // set size level
        sizeLevel = level;
        float size = 0.1f * Mathf.Pow(1.5f, level);
        transform.localScale = new Vector3(size, size, 1);
        // refresh force/speed
        moveForce = 4.0f * Mathf.Pow(1.4f, level);
        moveSpeed = 1.5f * Mathf.Pow(1.7f, level);
    }
    public void grow()
    {
        setSizeLevel(sizeLevel + 1);
    }
    public void reduce()
    {
        setSizeLevel(sizeLevel - 1);
    }
    public void bleed()
    {

    }
}
