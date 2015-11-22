using UnityEngine;
using System.Collections;

public class CamScr : MonoBehaviour
{
	public float camOffset = 2f;
	private GameObject engine;

	// Use this for initialization
	void Start ()
	{

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

		transform.position = new Vector3 (0, engine.transform.position.y - camOffset, -10);
	}
}
