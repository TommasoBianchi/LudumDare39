using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour {

	private int initialSouls = 100;

	[SerializeField]
	private bool followPlayer = false;


	public float hp {get; private set;}
	public float maxHp {get; private set;}

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

	[SerializeField]
	private int shieldSoulDrain;

	public int maxSoulNumber{get; private set;}

	public int souls {get; private set;}

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		souls = initialSouls;

	}
		

	// Update is called once per frame
	void Update () {
		soulDrainRate ();
		//startFollowPlayer ();
		endFollowPlayer ();
		/*if(follow){
			startFollowPlayer ();
		}
		else{
			endFollowPlayer();
		}
		*/
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
