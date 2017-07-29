using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

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


	private GameObject player;


	// Use this for initialization
	void Start (){
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//CROSS PLAT INPUT
		//WASD MOUSE 	
	}
		

	void OnTriggerStay2D(Collider2D collider){
		//BARRIER
		//RUIN ABILITIES
	}
}