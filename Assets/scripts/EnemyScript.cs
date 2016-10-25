using UnityEngine;
using System.Collections;

public class EnemyScript : PlayerScript
{  
    public YouScript player;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = player.position();
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        float dx = pos.x - myPos.x;
        float dy = pos.y - myPos.y;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(dx < 0 ? -moveSpeed : moveSpeed, dy < 0 ? -moveSpeed : moveSpeed);
        rb.velocity = Toolbox.Instance.confineVelocity(moveSpeed, rb.velocity);
		// adjust face direction
		adjustFace();
    }
	void adjustFace()
	{
		float theta = Mathf.Atan2(player.position().y - transform.position.y, player.position().x - transform.position.x);
		transform.rotation = Quaternion.Euler(0, 0, theta * 180.0f / Mathf.PI);
	}
}