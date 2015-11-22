using UnityEngine;
using System.Collections;

public class GameMgrScr : MonoBehaviour
{

	public GameObject track;
	public GameObject trackSwitch;
	public GameObject engine;
	public GameObject bg;
	public float rotationAngle = 28f;
	public float rotationOffset = 3f;
	public int mapSize = 3;
	public bool leftSwitchState = true;

	// Use this for initialization
	void Start ()
	{
		GenerateBG ();
		GenerateSwitches ();

		GameObject engineObj = (GameObject)Instantiate (engine, new Vector3 (0f, -2.5f, 0f), Quaternion.identity);

		GameObject[] switches = GameObject.FindGameObjectsWithTag ("Switch");
		foreach (GameObject switchObj in switches) {
			switchObj.transform.Rotate (Vector3.forward * rotationAngle);
			switchObj.transform.position = new Vector2 (switchObj.transform.position.x - rotationOffset, switchObj.transform.position.y);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			SwitchTracks ();
		}
	}
	
	private void GenerateBG ()
	{
		GameObject bgParent = GameObject.Find ("BGs");
		GameObject trackParent = GameObject.Find ("Tracks");
		float newY = -7.5f;

		//2 bg buffer on bottom of map
		for (int i = 0; i < mapSize+2; i++) {
			//Build BG
			/*float bgX = -10.2f;
			for (int j = 0; j < 3; j++) {
				Vector3 pos = new Vector3 (bgX, newY, 0);
				GameObject obj = (GameObject)GameObject.Instantiate (bg, pos, Quaternion.identity);
				obj.transform.parent = bgParent.transform;
				bgX += 10.2f;
			}
*/
			//Build Track
			float trackX = -4f;
			for (int j = 0; j < 5; j++) {
				Vector3 trackPos = new Vector3 (trackX, newY, 0);
				GameObject trackObj = (GameObject)GameObject.Instantiate (track, trackPos, Quaternion.identity);
				trackObj.transform.parent = trackParent.transform;
				trackX += 2f;
			}

			newY += 7.68f;
		}
	}

	private void GenerateSwitches ()
	{
		GameObject switchParent = GameObject.Find ("Switches");
		for (int i = 0; i < 10; i++) {
			int track = Random.Range (0, 5);
			float newX = -3f + 2 * track;
			float newY = Random.Range (0f, 7.5f * mapSize);
			Vector3 pos = new Vector3 (newX, newY, 0);
			GameObject obj = (GameObject)GameObject.Instantiate (trackSwitch, pos, Quaternion.identity);
			obj.transform.parent = switchParent.transform;
		}
	}

	private void SwitchTracks ()
	{
		GameObject[] switches = GameObject.FindGameObjectsWithTag ("Switch");

		if (leftSwitchState) {
			foreach (GameObject switchObj in switches) {
				switchObj.transform.rotation = Quaternion.Euler (Vector3.forward * -rotationAngle);
				switchObj.transform.position = new Vector2 (switchObj.transform.position.x + rotationOffset, switchObj.transform.position.y);
			}
		} else {
			foreach (GameObject switchObj in switches) {
				switchObj.transform.rotation = Quaternion.Euler (Vector3.forward * rotationAngle);
				switchObj.transform.position = new Vector2 (switchObj.transform.position.x - rotationOffset, switchObj.transform.position.y);
			}
		}

		leftSwitchState = !leftSwitchState;
	}
}
