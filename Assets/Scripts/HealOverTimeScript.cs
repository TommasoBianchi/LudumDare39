using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTimeScript : MonoBehaviour {

	private float timeLastUpdate = 0f;
	private float timeBetweenUpdates;
	private float healAmount;

	// Use this for initialization
	void Start () {
		timeBetweenUpdates = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ().healOverTimeTimeBetweenUpdates;
		healAmount = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ().healOverTimeAmount;
	}


	void OnTriggerStay2D(Collider2D collider){
		if (collider.CompareTag ("Player")) {
			if (Time.time >= timeLastUpdate + timeBetweenUpdates) {
				timeLastUpdate = Time.time;
				collider.gameObject.GetComponent<Player> ().hp += healAmount;
			}


		}
		if (collider.CompareTag ("Witch")) {
			collider.gameObject.GetComponent<Witch> ().hp += healAmount;
		}
	}
}
