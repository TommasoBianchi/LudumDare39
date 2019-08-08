using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class SoulScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MovementAI> ().target = GameObject.FindWithTag ("Witch").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag("Witch")) {
			coll.gameObject.GetComponent<Witch> ().Souls += 1;
			Destroy (gameObject);
		}
	}
}
