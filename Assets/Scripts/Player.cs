﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConeRaycaster))]
public class Player : MonoBehaviour, IDamageable
{
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

    private ConeRaycaster coneRaycaster;
    private bool isAttacking = false;
    private bool inside;
    private float firstTimeOut;

    public GameManager GM;

    void Start()
    {
        coneRaycaster = GetComponent<ConeRaycaster>();
        inside = true;
    }

    void Update()
    {
		outOfBarrierDamage ();

        if (!isAttacking)
        {
            wasdMovement();
            attack();
        }

    }

    void wasdMovement()
    {

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        transform.position += move * baseSpeed * Time.deltaTime;

        if ((move.x == 0) && move.y == 0)
        {
            PlayerAnimator.SetBool("Moving", false);
            PlayerAnimator.SetInteger("MoveDir", 5);
        }

        else if (Mathf.Abs(move.y) >= Mathf.Abs(move.x) && move.y >= move.x)
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

        else
        {
            PlayerAnimator.SetBool("Moving", false);
            PlayerAnimator.SetInteger("MoveDir", 5);
        }

    }

    void attack()
    {

        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButton(0))
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

            // Deal damage
            GameObject target = coneRaycaster.Raycast(dir);
            if (target != null)
            {
                IDamageable damageableTarget = target.GetComponent<IDamageable>();
                if (damageableTarget != null)
                {
                    damageableTarget.Damage(baseDamage);
                }
            }
        }
    }

    void onEndAttack()
    {
        isAttacking = false;
    }

    public void Damage(float damage)
    {
        hp -= damage;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        {
            firstTimeOut = Time.time;
            inside = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        { 
            inside = true;
        }
    }

    void outOfBarrierDamage()
    {
        if (!inside && (Time.time >= (firstTimeOut + GM.timeOutBeforeDMG)))
        {
            firstTimeOut = Time.time;
            hp -= GM.damageOutside;
        }
    }
}