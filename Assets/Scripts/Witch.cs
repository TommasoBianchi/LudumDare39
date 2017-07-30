using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using AICoreUnity;

public class Witch : MonoBehaviour {

	private int initialSouls = 100;
	public Animator animator;

	public float hp {get; set;}
	public float maxHp {get; set;}

	[SerializeField]
	private float baseSpeed;

	[SerializeField]
	private float baseDefence;

	[SerializeField]

	private float fogOfWarRadius;

	[SerializeField]
	private Dictionary<string, Ability> skillset;

	[SerializeField]
	private int shieldSoulDrain;

	public int maxSoulNumber{get; private set;}

	public int souls;

	private GameObject player;

	private GameObject barrier;


	private float accumulator = 0.0f;
	private float waitTime= 1.0f;

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
		
		accumulator += Time.deltaTime;
		if(accumulator >= waitTime){
			drainSouls ();
			accumulator -= waitTime;
		}


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

	private void drainSouls(){
	
		souls -= shieldSoulDrain;
	}
		
}
