using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	// "player" to follow
	public PlayerScript player;
	public float nearSize = 2.0f;
	public float farSize = 20.0f;

	// Use this for initialization
	void Start() {
	}

	void follow(Vector3 pos)
	{

		Camera camera = GetComponent<Camera>();
		Vector3 viewPoint = camera.WorldToViewportPoint(new Vector3(pos.x, pos.y, 0));
		Vector3 worldPoint = camera.ViewportToWorldPoint(viewPoint);
		transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
	}
	void zoom(bool inOut, float time)
	{
		Camera cam = GetComponent<Camera>();
		float curSize = cam.orthographicSize;
		if (inOut)
			curSize /= 1.1f;
		else
			curSize *= 1.1f;
		cam.orthographicSize = Toolbox.Instance.confine(nearSize, curSize, farSize);
	}
	// Update is called once per frame
	void Update()
	{
		follow(player.transform.position);
		// get mouse input (zoom in/out)
		float wheel = Input.GetAxis("Mouse ScrollWheel");
		if (wheel != 0)
			zoom(wheel > 0, 0.1f);
		if (Input.GetKeyDown(KeyCode.UpArrow))
			zoom(false, 0.1f);
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			zoom(true, 0.1f);
	}

}