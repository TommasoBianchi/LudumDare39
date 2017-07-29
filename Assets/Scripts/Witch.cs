using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Witch : MonoBehaviour {

	private int initialSouls = 100;


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
		spaceFollow ();		
		barrier ();

	}


	void soulDrainRate(){
		
	}	

	void barrier(){
	}

	void spaceFollow(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicSeek;
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicNone;
		}
	}
}
