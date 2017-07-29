using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
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
	private Dictionary<string, Ability> skillset;

	[SerializeField]
	private int shieldSoulDrain;

	public int maxSoulNumber{get; private set;}

	public int souls {get; set;}

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		souls = initialSouls;

	}
		

	// Update is called once per frame
	void Update () {
		soulDrainRate ();
		spaceFollow ();
		abilities ();

	}


	void soulDrainRate(){
		
	}	

	void spaceFollow(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicSeek;
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicNone;
		}
	}


	void abilities ()
	{

		if (Input.GetKeyDown (KeyCode.E)) {
			if (skillset.ContainsKey ("E")) {
				skillset ["E"].activate (gameObject);
			}

		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (skillset.ContainsKey ("Q")) {
				skillset ["Q"].activate (gameObject);
			}

		}
	}

	public bool soulCheck(int soul){
		return souls >= soul;
	}
}
