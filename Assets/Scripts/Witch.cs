using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using AICoreUnity;

public class Witch : MonoBehaviour, IDamageable {

	public Animator animator;
    private bool inside = true;
    private float firstTimeOut = 0f;

	[SerializeField]
	private float hp;
	public float Hp { 
		get {
			return hp;
		} set { 
			hp = value;
			if (hp > maxHp) {
				hp = maxHp;
			} else if (hp <= 0) {
				hp = 0;
				// TODO: gameover
			}
		}
	}
	public float maxHp;

	[SerializeField]
	private float baseSpeed;

	[SerializeField]
	private float baseDefence;

	[SerializeField]

	private float fogOfWarRadius;

	[SerializeField]
	private Dictionary<string, Ability> skillset = new Dictionary<string, Ability>();

	[SerializeField]
	private int shieldSoulDrain;

	[SerializeField]
	private int souls;
	public int Souls { get {
			return souls;
		} set {
			souls = value;
			if (souls <= 0) {
				souls = 0;
				barrier.SetActive (false);
                inside = false;
			} else {
				barrier.SetActive (true);
                inside = true;
			}
		}
	}
	public int maxSoulNumber;

    public GameManager GM;

	private GameObject player;
	private GameObject barrier;

	private float accumulator = 0.0f;
	[SerializeField]
	private float timeBetweenSoulDrain = 1.0f;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		barrier = GameObject.FindGameObjectWithTag ("Barrier");


		// TEMP
		skillset.Add("Q", new AbilityHealOverTime());
		skillset.Add("E", new AbilityHealOverTime());
	}
		

	// Update is called once per frame
	void Update () {
		soulDrainRate ();
		spaceFollow ();
		abilities ();
        outOfBarrierDamage();

	}


	void soulDrainRate(){
		
		accumulator += Time.deltaTime;
		if(accumulator >= timeBetweenSoulDrain){
			drainSouls ();
			accumulator -= timeBetweenSoulDrain;
		}


	}	

	void spaceFollow(){


		Vector3 position = player.transform.position - gameObject.transform.position;

        if ((position.x == 0) && position.y == 0)
        {
            animator.SetBool("Moving", false);
            animator.SetInteger("MoveDir", 5);
        }

        else if (Mathf.Abs(position.y) >= Mathf.Abs(position.x) && position.y >= position.x)
        {
            animator.SetInteger("MoveDir", 0);
        }

        else if (Mathf.Abs(position.x) >= Mathf.Abs(position.y) && position.x >= position.y)
        {
            animator.SetInteger("MoveDir", 1);
        }

        else if (Mathf.Abs(position.x) >= Mathf.Abs(position.y) && position.y >= position.x)
        {
            animator.SetInteger("MoveDir", 3);
        }

        else if (Mathf.Abs(position.y) >= Mathf.Abs(position.x) && position.x >= position.y)
        {
            animator.SetInteger("MoveDir", 2);
        }

        else
        {
            animator.SetBool("Moving", false);
            animator.SetInteger("MoveDir", 5);
        }

        if (Input.GetKeyDown (KeyCode.Space))
        {
			gameObject.GetComponent<MovementAI> ().target = GameObject.FindWithTag ("Player").GetComponent<Rigidbody2D> ();
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicSeek;

            animator.SetBool("Moving", true);
        }

        else if (Input.GetKeyUp (KeyCode.Space))
        {
			gameObject.GetComponent<MovementAI> ().aiAlgorithm = AIAlgorithm.KinematicNone;
            animator.SetBool("Moving", false);
        }
	}


	void abilities ()
	{

		if (Input.GetKeyDown (KeyCode.E)) {
			if (skillset.ContainsKey ("E") || skillset.ContainsKey ("e")) {
				skillset ["E"].activate (gameObject);
			}

		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (skillset.ContainsKey ("Q") || skillset.ContainsKey ("q")) {
				skillset ["Q"].activate (gameObject);
			}

		}
	}

	public bool areSoulsEnough(int soul){
		return soul <= Souls;
	}

	private void drainSouls(){
		Souls -= shieldSoulDrain;
	}


    public void Damage(float damage)
    {
        Hp -= damage;
    }

    void outOfBarrierDamage ()
    {
        if (!inside && (Time.time >= (firstTimeOut + GM.timeOutBeforeDMG)))
        {
            firstTimeOut = Time.time;
            hp -= GM.damageOutside;
        }
    }
}



/* TODO
 * 
 * 
 * ATTACK WITH ABI:ITIES
 *  if (Input.GetMouseButton(0))
        {
            PlayerAnimator.SetInteger("MoveDir", -1);
            isAttacking = true;

            if ((dir.x == 0) && dir.y == 0)
            {
                PlayerAnimator.SetInteger("AttDir", 5);
            }

            else if (Mathf.Abs(dir.y) >= Mathf.Abs(dir.x) && dir.y >= dir.x)
            {
                PlayerAnimator.SetInteger("AttDir", 0);
                PlayerAnimator.SetTrigger("kill");
            }

            else if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y) && dir.x >= dir.y)
            {
                PlayerAnimator.SetInteger("AttDir", 1);
                PlayerAnimator.SetTrigger("kill");
            }

            else if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y) && dir.y >= dir.x)
            {
                PlayerAnimator.SetInteger("AttDir", 3);
                PlayerAnimator.SetTrigger("kill");
            }

            else if (Mathf.Abs(dir.y) >= Mathf.Abs(dir.x) && dir.x >= dir.y)
            {
                PlayerAnimator.SetInteger("AttDir", 2);
                PlayerAnimator.SetTrigger("kill");
            }

            else
            {
                PlayerAnimator.SetInteger("AttDir", 5);
            }

        }


*/