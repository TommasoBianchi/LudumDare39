using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusBarrierOnWitch : MonoBehaviour {


	GameObject witch;
	// Use this for initialization
	void Start () {
		witch = GameObject.FindGameObjectWithTag ("Witch");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = witch.transform.position;
	}
}
