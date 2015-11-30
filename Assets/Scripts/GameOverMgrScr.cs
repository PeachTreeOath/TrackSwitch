using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMgrScr : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject obj = GameObject.Find ("GlobalMgr");
		if (obj != null) {
			GlobalMgrScr gm = obj.GetComponent<GlobalMgrScr> ();
			Text text = GameObject.Find ("Stage").GetComponent<Text> ();
			text.text = "You reached\nStage " + gm.stage + "!";

			if (gm.stage / 5 > gm.highestCheckpoint) {
				Text checkText = GameObject.Find ("Checkpoint").GetComponent<Text> ();
				checkText.text = "Checkpoint reached at stage " + gm.stage + "!";

				gm.highestCheckpoint = gm.stage / 5;
			}

			if (gm.highestCheckpoint != 0) {
				gm.stage = gm.highestCheckpoint * 5;
			} else {
				gm.stage = 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
