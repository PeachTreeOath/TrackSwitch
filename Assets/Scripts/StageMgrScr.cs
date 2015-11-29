using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageMgrScr : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject obj = GameObject.Find ("GlobalMgr");
		if (obj != null) {
			GlobalMgrScr gm = obj.GetComponent<GlobalMgrScr> ();
			Text text = GameObject.Find ("Stage").GetComponent<Text> ();
			text.text = "Stage " + gm.stage;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
