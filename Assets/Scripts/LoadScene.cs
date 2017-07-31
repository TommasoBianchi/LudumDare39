using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	public float AfterSeconds;
	public string NextSceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AfterSeconds >= 0 && Time.timeSinceLevelLoad > AfterSeconds) {
			Application.LoadLevel (NextSceneName);
		}
	}

	public void LoadCutScene() {
		Application.LoadLevel (NextSceneName);
	}
}
