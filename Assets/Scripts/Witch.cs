using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour {

	private int initialSouls = 100;

	private Transform player;

	[SerializeField]
	private int shieldSoulDrain = 1;

	[SerializeField]
	private float hp;

	[SerializeField]
	private float baseDamage;

	[SerializeField]
	private float baseSpeed;

	[SerializeField]
	private float barrierRadius;

	[SerializeField]
	private float baseDefence;

	[SerializeField]
	private float fogOfWarRadius;

	[SerializeField]
	private Dictionary<string , string> skillset;

	public int souls {get; private set;}



	private GameObject witch;

	// Use this for initialization
	void Start () {
		witch = GameObject.FindGameObjectWithTag ("Witch");
		player = GameObject.FindGameObjectWithTag ("Player");
		souls = initialSouls;

	}
		

	// Update is called once per frame
	void Update () {
		soulDrainRate ();
		startFollowPlayer ();
		endFollowPlayer ();
	}


	public Vector3 offset = new Vector3(0f, 7.5f, 0f);


	private void LateUpdate()
	{
		transform.position = target.position + offset;
	}


	void startFollowPlayer(){
	


	}

	void endFollowPlayer(){

	}


	void soulDrainRate(){
		
	}	

	void OnTriggerStay2D(Collider2D collider){
		//BARRIER
		//RUIN ABILITIES
	}
}
