using UnityEngine;
using System.Collections;

public class HeartScr : MonoBehaviour {

	private float scale = 0;
	private float inc = 0.01f;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector2(scale,scale);
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (scale < 1) {
			scale += inc;
			transform.localScale = new Vector2(scale,scale);
		}

	}
}
