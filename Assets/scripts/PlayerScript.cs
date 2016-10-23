using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{  //Player movement
    public float moveSpeed = 2.0f;
    public float size = 0.1f;
	// Use this for initialization
	void Start ()
    {
	}

    void onDirection(int d, bool move=true)
    {
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        float[ , ] speeds = new float[4, 2] { {0, moveSpeed}, {moveSpeed,0 }, {0,-moveSpeed }, {-moveSpeed,0 } };
        if (move)
            rb.velocity = new Vector2(speeds[d, 0] != 0 ? speeds[d, 0] : rb.velocity.x, speeds[d, 1] != 0 ? speeds[d, 1] : rb.velocity.y);
        else
            rb.velocity = new Vector2(speeds[d, 0] != 0 ? 0 : rb.velocity.x, speeds[d, 1] != 0 ? 0 : rb.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            onDirection(0);
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            onDirection(0, false);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            onDirection(1);
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            onDirection(1, false);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            onDirection(2);
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            onDirection(2, false);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            onDirection(3);
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            onDirection(3, false);

    }
    public Vector2 position()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
