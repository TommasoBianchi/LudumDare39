using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float hp;

	[SerializeField]
	private float baseDamage;

	[SerializeField]
	private float baseSpeed;

	[SerializeField]
	private float attackFrequency;

	[SerializeField]
	private float baseDefence;

	[SerializeField]
	private float fogOfWarRadius;

	[SerializeField]
	private int souls;



	private string name;

	private GameObject player;
	private GameObject witch;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		witch =  GameObject.FindGameObjectWithTag ("Witch");
	}
	
	// Update is called once per frame
	void Update () {


		seekClosest ();

	}


	void seekClosest(){
		Vector3 playerOffset;
		Vector3 witchOffset;
		int playerDistance;
		int witchDistance;


		playerOffset = player.transform.position - gameObject.transform.position;
		witchOffset = witch.transform.position - gameObject.transform.position;

		playerDistance = (int) (Mathf.Abs(playerOffset.x) + Mathf.Abs(playerOffset.y));
		witchDistance = (int)(Mathf.Abs(witchOffset.x) + Mathf.Abs(witchOffset.y));

		if (playerDistance < witchDistance) {
			gameObject.GetComponent<MovementAI> ().target = player.GetComponent<Rigidbody2D>();
		} else {
			gameObject.GetComponent<MovementAI> ().target = witch.GetComponent<Rigidbody2D>();
		}
	}
		
}
