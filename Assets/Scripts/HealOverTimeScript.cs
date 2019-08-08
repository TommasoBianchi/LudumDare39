using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTimeScript : MonoBehaviour {

	private float timeLastUpdate = 0f;
	private float timeBetweenUpdates;
	private float healAmount;

	private float timeOfCreation;
	private float timeToLive;

	// Use this for initialization
	void Start () {
		SkillManager sm = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ();
		timeBetweenUpdates = sm.healOverTimeTimeBetweenUpdates;
		healAmount = sm.healOverTimeAmount;
		timeOfCreation = Time.time;
		timeToLive = sm.healOverTimeDuration;
	}

	void Update() {
		if (Time.time > timeOfCreation + timeToLive) {
			Destroy (gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D collider){
		if (collider.CompareTag ("Player")) {
			if (Time.time >= timeLastUpdate + timeBetweenUpdates) {
				timeLastUpdate = Time.time;
				collider.gameObject.GetComponent<Player> ().Hp += healAmount;
			}


		}
		if (collider.CompareTag ("Witch")) {
			collider.gameObject.GetComponent<Witch> ().Hp += healAmount;
		}
	}
}
