using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICoreUnity;

public class Enemy : MonoBehaviour, IDamageable {

    [SerializeField]
    private float baseDamage;

    [SerializeField]
    private float baseHealth;

    public float hp { get; set; }

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private float attackFrequency;

    [SerializeField]
    private float baseDefence;

    [SerializeField]
    private float fogOfWarRadius;

    [SerializeField]
    private int souls;



    private string name;

    private GameObject player;
    private GameObject witch;

	[SerializeField]
	public SoulScript SoulObject;

    // Use this for initialization
    void Start()
    {
        hp = baseHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        witch = GameObject.FindGameObjectWithTag("Witch");
    }

    // Update is called once per frame
    void Update()
    {
        seekClosest();
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

        if (playerDistance < witchDistance)
        {
            gameObject.GetComponent<MovementAI>().target = player.GetComponent<Rigidbody2D>();
        }
        else
        {
            gameObject.GetComponent<MovementAI>().target = witch.GetComponent<Rigidbody2D>();
        }
    }



    private void checkHealth()
    {
        if (hp <= 0)
        {
			for (int i = 0; i < souls; i++) {
				Instantiate (SoulObject, transform.position + new Vector3 (Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0), Quaternion.identity);
			}
            Destroy(gameObject);
        }
    }


    public void Damage(float damage)
    {
        hp -= damage;
        Debug.Log("Hp: " + hp);
        checkHealth();
    }
}
