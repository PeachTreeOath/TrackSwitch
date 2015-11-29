using UnityEngine;
using System.Collections;

public class EngineScr : MonoBehaviour
{
	public GameObject explosion;
	public float accelRate = 0.1f;
	public float speed = 2f;
	public bool alive;
	private float rotationAngle;
	private float currAngle;
	private float accel = 0f;
	private GameMgrScr gameMgr;
	private Rigidbody2D body;
	// 1 = left
	// 2 = straight
	// 3 = right
	private int currLane = 3;
	private int destLane = 3;

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
		if (alive) {
			if (accel < speed) {
				accel += accelRate;
			}
		}
	}

	void FixedUpdate ()
	{
		if (alive) {
			if (accel < speed) {
				accel += accelRate * Time.fixedDeltaTime;
			}

			body.angularVelocity = 0;
			body.velocity = gameObject.transform.up * accel;

			if (body.velocity.magnitude > speed) {
				body.velocity = body.velocity.normalized * speed;
			}
		} 
	}

	void LateUpdate ()
	{
		if (alive) {
			if (currLane > destLane) {
				switch (destLane) {
				case 0:
					if (transform.position.x <= -6) {
						Blowup ();
					}
					break;
				case 1:
					if (transform.position.x <= -4) {
						transform.position = new Vector2 (-4, transform.position.y); 
						Turn (2);
					}
					break;
				case 2:
					if (transform.position.x <= -2) {
						transform.position = new Vector2 (-2, transform.position.y);
						Turn (2);
					}
					break;
				case 3:
					if (transform.position.x <= 0) {
						transform.position = new Vector2 (0, transform.position.y);
						Turn (2);
					}
					break;
				case 4:
					if (transform.position.x <= 2) {
						transform.position = new Vector2 (2, transform.position.y);
						Turn (2);
					}
					break;
				}
			} else if (currLane < destLane) {
				switch (destLane) {
				case 2:
					if (transform.position.x >= -2) {
						transform.position = new Vector2 (-2, transform.position.y);
						Turn (2);
					}
					break;
				case 3:
					if (transform.position.x >= 0) {
						transform.position = new Vector2 (0, transform.position.y);
						Turn (2);
					}
					break;
				case 4:
					if (transform.position.x >= 2) {
						transform.position = new Vector2 (2, transform.position.y);
						Turn (2);
					}
					break;
				case 5:
					if (transform.position.x >= 4) {
						transform.position = new Vector2 (4, transform.position.y);
						Turn (2);
					}
					break;
				case 6:
					if (transform.position.x >= 6) {
						Blowup ();
					}
					break;
				}
			}
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
		} else if (collider.gameObject.tag == "Obstacle") {
			Blowup ();
		}
		else if (collider.gameObject.tag == "Terminal") {
			Victory ();
		}
	}

	// 1 = left
	// 2 = straight
	// 3 = right
	private void Turn (int dir)
	{
		switch (dir) {
		case 1:
			transform.rotation = Quaternion.Euler (Vector3.forward * rotationAngle);
			currAngle = rotationAngle * Mathf.Deg2Rad;
			destLane = currLane - 1;
			gameMgr.freezeTracks = true;
			break;
		case 2:
			transform.rotation = Quaternion.Euler (Vector3.up);
			currAngle = 0f;
			currLane = destLane;
			gameMgr.freezeTracks = false;
			break;
		case 3:
			transform.rotation = Quaternion.Euler (Vector3.forward * -rotationAngle);
			currAngle = -rotationAngle * Mathf.Deg2Rad;
			destLane = currLane + 1;
			gameMgr.freezeTracks = true;
			break;
		}
	}

	private void Blowup ()
	{
		alive = false;
		body.velocity = new Vector2 (0, 0);
		gameMgr.freezeTracks = true;
		Instantiate (explosion, transform.position, Quaternion.Euler (Vector3.forward));

		Invoke ("GameOver", 1.5f);
	}

	private void GameOver()
	{
		Application.LoadLevel ("GameOver");
	}

	private void Victory()
	{
		alive = false;
		body.velocity = new Vector2 (0, 0);
		gameMgr.freezeTracks = true;
		gameMgr.LaunchHeart ();

		Invoke ("NextLevel", 1.5f);
	}

	private void NextLevel()
	{
		GameObject obj = GameObject.Find ("GlobalMgr");
		if (obj != null) {
			GlobalMgrScr gm = obj.GetComponent<GlobalMgrScr> ();
			gm.stage++;
		}

		Application.LoadLevel ("StageIntro");
	}
}
