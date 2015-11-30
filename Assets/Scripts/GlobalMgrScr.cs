using UnityEngine;
using System.Collections;

public class GlobalMgrScr : MonoBehaviour {
	
	public int stage = 1;
	public int highestCheckpoint = 0;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
