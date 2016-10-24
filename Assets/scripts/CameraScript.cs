using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	// "player" to follow
	public PlayerScript player;

	// Use this for initialization
	void Start() {
	}

	void follow(Vector2 pos)
	{
		Camera camera = GetComponent<Camera>();
		Vector3 viewPoint = camera.WorldToViewportPoint(new Vector3(pos.x, pos.y, 0));
		Vector3 worldPoint = camera.ViewportToWorldPoint(viewPoint);
		transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
	}
	void zoom(int level, float time)
	{

	}
	// Update is called once per frame
	void Update()
	{
		follow(player.position());
	}

}