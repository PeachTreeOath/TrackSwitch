using UnityEngine;
using System.Collections;

public class ExplosionScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayAndDisappear ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void PlayAndDisappear(){
		Destroy (gameObject, 0.5f);
	}
}
