using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D collider){
	
		//ATTACK WITCH PLAYER
	}
}
