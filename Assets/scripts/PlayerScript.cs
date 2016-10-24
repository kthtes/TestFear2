using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{  //Player movement
    public float moveForce = 5.0f;
    public float moveSpeed = 2.0f;
    public float size = 0.1f;
    //
    float[] curForces;
	// Use this for initialization
	void Start ()
    {
        curForces = new float[4] { 0, 0, 0, 0 };
	}

    void onDirection(int d, bool move=true)
    {
        curForces[d] = move ? moveForce : 0;
        ConstantForce2D f = GetComponent<ConstantForce2D>();
        f.force = new Vector2(curForces[1] - curForces[3], curForces[0] - curForces[2]);
    }
    // Update is called once per frame
    void Update()
    {
        onDirection(0, Input.GetKey(KeyCode.W));
        onDirection(1, Input.GetKey(KeyCode.D));
        onDirection(2, Input.GetKey(KeyCode.S));
        onDirection(3, Input.GetKey(KeyCode.A));

        // check speed limit
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Toolbox.Instance.confineVelocity(moveSpeed, rb.velocity);
    }
    public Vector2 position()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
