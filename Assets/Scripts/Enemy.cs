using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float baseDamage;

    [SerializeField]
    private float baseHealth;

    public float hp { get; set; }

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float baseDefence;

    [SerializeField]
    private float fogOfWarRadius;

    [SerializeField]
    private int minSouls;
    [SerializeField]
    private int maxSouls;

    private Color oldColor;
    private float oldColorTimeChange;
    private float oldColorTimeBetween = 0.3f;
    private bool isRed = false;

    private string name;

    private GameObject player;
    private GameObject witch;

    [SerializeField]
    public SoulScript SoulObject;
    public ParticleSystem DeathEffect;

    public Animator animator;

    private Vector3 oldPos;

    private ConeRaycaster coneRaycaster;

    // Use this for initialization
    void Start()
    {
        hp = baseHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        witch = GameObject.FindGameObjectWithTag("Witch");
        coneRaycaster = GetComponent<ConeRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        seekClosest();
        checkDeactivateDamageRedEffect();
    }


    void seekClosest()
    {

        Vector3 playerOffset;
        Vector3 witchOffset;
        float playerDistance;
        float witchDistance;

        playerOffset = player.transform.position - gameObject.transform.position;
        witchOffset = witch.transform.position - gameObject.transform.position;

        playerDistance = Mathf.Abs(playerOffset.x) + Mathf.Abs(playerOffset.y);
        witchDistance = Mathf.Abs(witchOffset.x) + Mathf.Abs(witchOffset.y);

        if (witch.GetComponent<Witch>().Souls > 0 && (witch.transform.position - player.transform.position).sqrMagnitude < 50)
        {
            // Barrier is on and player is into it, so chase the barrier by chasing the witch
            witchDistance = -1;
        }

        if (playerDistance < witchDistance)
        {
            gameObject.GetComponent<MovementAI>().satisfactionRadius = 2;
            gameObject.GetComponent<MovementAI>().target = player.GetComponent<Rigidbody2D>();

            if (transform.position.Equals(oldPos))
            {
                Attack(player.transform.position - transform.position);
            }

            else if (Mathf.Abs(playerOffset.y) >= Mathf.Abs(playerOffset.x) && playerOffset.y >= playerOffset.x)
            {
                animator.SetInteger("MoveDir", 0);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(playerOffset.x) >= Mathf.Abs(playerOffset.y) && playerOffset.x >= playerOffset.y)
            {
                animator.SetInteger("MoveDir", 1);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(playerOffset.x) >= Mathf.Abs(playerOffset.y) && playerOffset.y >= playerOffset.x)
            {
                animator.SetInteger("MoveDir", 3);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(playerOffset.y) >= Mathf.Abs(playerOffset.x) && playerOffset.x >= playerOffset.y)
            {
                animator.SetInteger("MoveDir", 2);
                animator.SetBool("Moving", true);
            }

            else
            {
                animator.SetBool("Moving", false);
                animator.SetInteger("MoveDir", 5);
            }
        }
        else
        {
            if (witch.GetComponent<Witch>().Souls > 0)
                gameObject.GetComponent<MovementAI>().satisfactionRadius = 8 + transform.localScale.x;
            else
                gameObject.GetComponent<MovementAI>().satisfactionRadius = 2;

            gameObject.GetComponent<MovementAI>().target = witch.GetComponent<Rigidbody2D>();

            if (transform.position.Equals(oldPos))
            {
                Attack(witch.transform.position - transform.position);
            }

            else if (Mathf.Abs(witchOffset.y) >= Mathf.Abs(witchOffset.x) && witchOffset.y >= witchOffset.x)
            {
                animator.SetInteger("MoveDir", 0);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(witchOffset.x) >= Mathf.Abs(witchOffset.y) && witchOffset.x >= witchOffset.y)
            {
                animator.SetInteger("MoveDir", 1);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(witchOffset.x) >= Mathf.Abs(witchOffset.y) && witchOffset.y >= witchOffset.x)
            {
                animator.SetInteger("MoveDir", 3);
                animator.SetBool("Moving", true);
            }

            else if (Mathf.Abs(witchOffset.y) >= Mathf.Abs(witchOffset.x) && witchOffset.x >= witchOffset.y)
            {
                animator.SetInteger("MoveDir", 2);
                animator.SetBool("Moving", true);
            }

            else
            {
                animator.SetBool("Moving", false);
                animator.SetInteger("MoveDir", 5);
            }
        }
        oldPos = transform.position;
    }

    private float nextAttackTime;

    void Attack(Vector2 dir)
    {
        if (Time.realtimeSinceStartup < nextAttackTime)
            return;

        nextAttackTime = Time.realtimeSinceStartup + attackDelay;

        animator.SetBool("Moving", false);
        animator.SetInteger("MoveDir", 5);
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

    private void checkHealth()
    {
        if (hp <= 0)
        {
            int numSouls = Random.Range(minSouls, maxSouls + 1);
            for (int i = 0; i < numSouls; i++)
            {
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                Instantiate(SoulObject, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }


    public void Damage(float damage)
    {
        hp -= damage;
        checkHealth();
        activateDamageRedEffect();
        Vector3 dir = Input.mousePosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce((new Vector2(dir.x, dir.y)) * 0.01f, ForceMode2D.Impulse);
    }

    private void activateDamageRedEffect()
    {
        oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        oldColorTimeChange = Time.time;
        isRed = true;
    }

    private void checkDeactivateDamageRedEffect()
    {
        if (isRed && Time.time > oldColorTimeChange + oldColorTimeBetween)
        {
            GetComponent<SpriteRenderer>().color = oldColor;
            isRed = false;
        }
    }
}
