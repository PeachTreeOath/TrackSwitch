using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMgrScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("GlobalMgr");
		if (obj != null) {
			GlobalMgrScr gm = obj.GetComponent<GlobalMgrScr> ();
			Text text = GameObject.Find ("Stage").GetComponent<Text> ();
			text.text = "You reached\nStage " + gm.stage + "!";

			gm.stage = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
