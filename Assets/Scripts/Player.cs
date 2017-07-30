using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float startHp = 100f;
	public float hp {get; set;}
	public float maxHp;
    public Animator PlayerAnimator;

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
		hp = startHp;
	}
	
	// Update is called once per frame
	void Update () {
		
		wasdMovement();
		damage ();
	}
		
	void wasdMovement(){

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		transform.position += move * baseSpeed * Time.deltaTime;

        if(Mathf.Abs(move.y)>Mathf.Abs(move.x) && move.y>move.x)
        {
            PlayerAnimator.SetInteger("MoveDir", 0);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.x > move.y)
        {
            PlayerAnimator.SetInteger("MoveDir", 1);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.y > move.x)
        {
            PlayerAnimator.SetInteger("MoveDir", 3);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.y) > Mathf.Abs(move.x) && move.x > move.y)
        {
            PlayerAnimator.SetInteger("MoveDir", 2);
            PlayerAnimator.SetBool("Moving", true);
        }

        else {
            PlayerAnimator.SetBool("Moving", false);
            PlayerAnimator.SetInteger("MoveDir", 5);
        }

    }

	void damage(){
		if (Input.GetMouseButton (0)) {
			Debug.Log ("DAMAGE");
            PlayerAnimator.Play("PlayerAttackRight");
		}
	}
}