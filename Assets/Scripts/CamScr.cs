using UnityEngine;
using System.Collections;

public class CamScr : MonoBehaviour
{
	public float panSpeed = 0.05f;
	public float zoomSpeed = 0.2f;
	private float camOffset = -2.5f;
	private bool isPan = true;
	private GameObject engine;
	private Camera cam;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void LateUpdate ()
	{
		if (engine == null) {
			engine = GameObject.Find ("EnginePf(Clone)");
		}

		if (!isPan) {
			if(cam.orthographicSize > 3.5f)
			{
				cam.orthographicSize -= zoomSpeed;
			}
			transform.position = new Vector3 (0, engine.transform.position.y - camOffset, -10);
		} else {
			transform.position = new Vector3 (transform.position.x, transform.position.y - panSpeed, -10);
			if (transform.position.y <= 0.5f) {
				engine.GetComponent<EngineScr> ().alive = true;
				isPan = false;
			}
		}
	}

	public void PanCam (Vector3 start)
	{
		cam.orthographicSize = 7f;
		transform.position = new Vector3(start.x, start.y+5f, -10) ;
		isPan = true;
	}
}
