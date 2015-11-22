using UnityEngine;
using System.Collections;

public class EngineScr : MonoBehaviour
{
	public float accelRate = 0.1f;
	public float speed = 2f;

	private float rotationAngle;
	private float currAngle;
	private float accel = 0f;
	private GameMgrScr gameMgr;
	private Rigidbody2D body;
	// 1 = left
	// 2 = straight
	// 3 = right
	private int facing = 2;

	// Use this for initialization
	void Start ()
	{
		gameMgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();
		body = GetComponent<Rigidbody2D> ();
		rotationAngle = gameMgr.rotationAngle;
		currAngle = rotationAngle * Mathf.Deg2Rad;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (accel < speed) {
			accel += accelRate;
		}
	}

	void FixedUpdate()
	{
		if (accel < speed) {
			accel += accelRate*Time.fixedDeltaTime;
		}

		body.angularVelocity = 0;
		body.velocity = gameObject.transform.up * accel;

		if (body.velocity.magnitude > speed) {
			body.velocity = body.velocity.normalized * speed;
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.GetComponent<SwitchEntranceScr> () != null) {
			if (gameMgr.leftSwitchState) {
				Turn (1);
			} else {
				Turn (3);
			}
		} else if (collider.gameObject.GetComponent<SwitchExitScr> () != null) {
			Turn (2);
		}
	}

	// 1 = left
	// 2 = straight
	// 3 = right
	private void Turn (int dir)
	{
		facing = dir;
		switch (dir) {
		case 1:
			transform.rotation = Quaternion.Euler (Vector3.forward * rotationAngle);
			currAngle = rotationAngle * Mathf.Deg2Rad;
			break;
		case 2:
			transform.rotation = Quaternion.Euler (Vector3.forward);
			currAngle = 0f;
			break;
		case 3:
			transform.rotation = Quaternion.Euler (Vector3.forward * -rotationAngle);
			currAngle = -rotationAngle * Mathf.Deg2Rad;
			break;
		}
	}
}
