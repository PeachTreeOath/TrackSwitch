using UnityEngine;
using System.Collections;

public class GameMgrScr : MonoBehaviour
{
	public GameObject track;
	public GameObject trackSwitch;
	public GameObject engine;
	public GameObject bg;
	public GameObject obs1;
	public GameObject terminal;
	public GameObject heart;
	public float rotationAngle = 28f;
	public float rotationOffset = 3f;
	public int mapSize = 3;
	public bool leftSwitchState = true;
	public bool includeBG = false;
	public bool freezeTracks = false;
	// 0 = empty
	// 1 = obstacle
	// 2 = switch
	private int[,] objMap;
	private Vector3 terminalPos;
	private CamScr cam;
	private EngineScr train;

	// Use this for initialization
	void Start ()
	{
		cam = GameObject.Find ("Main Camera").GetComponent<CamScr> ();
		GameObject engineObj = (GameObject)Instantiate (engine, new Vector3 (0f, -2.5f, 0f), Quaternion.identity);
		train = engineObj.GetComponent<EngineScr> ();
		
		SetStageVars ();

		GenerateBG ();
		GenerateObjMap ();
		PlaceTerminal ();
		cam.PanCam (terminalPos);
		
		GameObject[] switches = GameObject.FindGameObjectsWithTag ("Switch");
		foreach (GameObject switchObj in switches) {
			switchObj.transform.Rotate (Vector3.forward * rotationAngle);
			switchObj.transform.position = new Vector2 (switchObj.transform.position.x - rotationOffset, switchObj.transform.position.y);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!freezeTracks) {
			if (Input.GetMouseButtonDown (0)) {
				SwitchTracks ();
			}
		}
	}

	private void SetStageVars ()
	{
		int stage = 1;
		GameObject obj = GameObject.Find ("GlobalMgr");
		if (obj != null) {
			GlobalMgrScr gm = obj.GetComponent<GlobalMgrScr> ();
			stage = gm.stage;
		}

		mapSize += stage;
		cam.panSpeed = 0.05f + stage * 0.005f;
		train.speed = 2f + stage * 0.5f;
	}

	private void GenerateBG ()
	{
		GameObject bgParent = GameObject.Find ("BGs");
		GameObject trackParent = GameObject.Find ("Tracks");
		float newY = -7.5f;

		//2 bg buffer on bottom of map
		for (int i = 0; i < mapSize+5; i++) {
			//Build BG
			if (includeBG) {
				float bgX = -10.2f;
				for (int j = 0; j < 3; j++) {
					Vector3 pos = new Vector3 (bgX, newY, 0);
					GameObject obj = (GameObject)GameObject.Instantiate (bg, pos, Quaternion.identity);
					obj.transform.parent = bgParent.transform;
					bgX += 10.2f;
				}
			}
			
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

	private void GenerateObjMap ()
	{
		objMap = new int[mapSize, 5];
		GenerateSwitch (0, 2);
		objMap [0, 2] = 2;
		for (int row = 0; row < mapSize; row++) {
			int track;

			// Create i# of switches
			int roll = Random.Range (0, 100);
			int numSwitches = 0;
			if(roll < 25)
			{
				numSwitches = 1;
			}
			else if(roll < 60)
			{
				numSwitches = 2;
			}
			else if(roll < 80)
			{
				numSwitches = 3;
			}

			for (int i = 0; i < numSwitches; i++) {
				track = FindOpenTrack (row);
				if(track == -1) break;
				objMap [row, track] = 2;
				GenerateSwitch (row, track);
			}

			roll = Random.Range (0, 100);
			int numObstacles = 0;
			if(roll < 25)
			{
				numObstacles = 1;
			}
			else if(roll < 65)
			{
				numObstacles = 2;
			}
			else if(roll < 85)
			{
				numObstacles = 3;
			}
			else if(roll < 98)
			{
				numObstacles = 4;
			}
			// Create i# of obstacles
			for (int i = 0; i < numObstacles; i++) {
				// Allow 3 retries for placement
				for (int j = 0; j < 2; j++) {
					track = FindOpenTrack (row);
					if(track == -1) break;
					objMap [row, track] = 1;
					if (IsMapCompletable (0, 2)) {
						GenerateObstacle (row, track);
						break;
					} else {
						objMap [row, track] = 0;
					}
				}
			}
		}
	}

	private int FindOpenTrack (int row)
	{
		for (int i = 0; i < 5; i++) {
			int track = Random.Range (0, 5);
			if (objMap [row, track] != 0) {
				continue;
			} else {
				return track;
			}
		}
		return -1;
	}

	private void PlaceTerminal ()
	{
		float y = 7.5f * mapSize + 4;
		Vector3 pos = new Vector3 (0, y, 0);
		terminalPos = pos;
		GameObject.Instantiate (terminal, pos, Quaternion.identity);
	}

	private void GenerateObstacle (int row, int track)
	{
		GameObject obstacleParent = GameObject.Find ("Obstacles");
		float newX = -4f + 2 * track;
		float newY = 7.5f * row + 4f;
		Vector3 pos = new Vector3 (newX, newY, 0);
		GameObject obj = (GameObject)GameObject.Instantiate (obs1, pos, Quaternion.identity);
		obj.transform.parent = obstacleParent.transform;
	}

	private void GenerateSwitch (int row, int track)
	{
		GameObject switchParent = GameObject.Find ("Switches");
		float newX = -3f + 2 * track;
		float newY = 7.5f * row + 6f;
		Vector3 pos = new Vector3 (newX, newY, 0);
		GameObject obj = (GameObject)GameObject.Instantiate (trackSwitch, pos, Quaternion.identity);
		obj.transform.parent = switchParent.transform;
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

	private bool IsMapCompletable (int row, int track)
	{
		if (track == -1 || track == 5) {
			return false;
		}
		if (row == mapSize) {
			return true;
		}
		if (objMap [row, track] == 1) {
			return false;
		}

		if (objMap [row, track] == 2) {
			bool lPath = IsMapCompletable (row + 1, track - 1);
			bool rPath = IsMapCompletable (row + 1, track + 1);

			return lPath || rPath;
		} else {
			return IsMapCompletable (row + 1, track);
		}
	}

	public void LaunchHeart ()
	{
		GameObject.Instantiate (heart, terminalPos, Quaternion.identity);
	}
}
