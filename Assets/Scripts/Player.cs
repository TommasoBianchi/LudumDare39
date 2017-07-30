using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float startHp;
	public float hp;
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
		wasdMovement();
		damage ();
	}
		
	void wasdMovement(){

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		transform.position += move * baseSpeed * Time.deltaTime;

        if ((move.x == 0) && move.y == 0)
        {
            PlayerAnimator.SetBool("Moving", false);
            PlayerAnimator.SetInteger("MoveDir", 5);
        }

        else if(Mathf.Abs(move.y) >= Mathf.Abs(move.x) && move.y >= move.x)
        {
            PlayerAnimator.SetInteger("MoveDir", 0);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.x) >= Mathf.Abs(move.y) && move.x >= move.y)
        {
            PlayerAnimator.SetInteger("MoveDir", 1);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.x) >= Mathf.Abs(move.y) && move.y >= move.x)
        {
            PlayerAnimator.SetInteger("MoveDir", 3);
            PlayerAnimator.SetBool("Moving", true);
        }

        else if (Mathf.Abs(move.y) >= Mathf.Abs(move.x) && move.x >= move.y)
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

        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		if (Input.GetMouseButton (0)) {
			Debug.Log ("DAMAGE");

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
    }
}