using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour {

	private float damageAmount;
	private float speed;
	private Vector2 dir;
	private FogOfWar fow;

	void Start () {
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		dir = new Vector2 (diff.x, diff.y);
		dir /= dir.magnitude;
		damageAmount = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ().fireballDamage;
		speed = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ().fireballSpeed;
		fow = GameObject.FindWithTag ("FOW").GetComponent<FogOfWar> ();
		fow.revealer3 = gameObject.transform;
	}

	void Update() {
		transform.Translate (dir * speed);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag("Enemy")) {
			collider.gameObject.GetComponent<IDamageable> ().Damage(damageAmount);
		}
	}
}
