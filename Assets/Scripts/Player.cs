using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float hp {get; private set;}
	public float maxHp {get; private set;}

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


	// Use this for initialization
	void Start (){
		
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