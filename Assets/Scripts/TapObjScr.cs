using UnityEngine;
using System.Collections;

public class TapObjScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			string scene = Application.loadedLevelName;

			switch(scene)
			{
			case("Title"):
				Application.LoadLevel ("StageIntro");
				break;
			case("StageIntro"):
				Application.LoadLevel ("Game");
				break;
			case("GameOver"):
				Application.LoadLevel ("StageIntro");
				break;
			}
		}
	}
}
