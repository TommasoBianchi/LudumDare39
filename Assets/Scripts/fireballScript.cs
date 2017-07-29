using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour {

	private float damageAmount;

	void Start () {
		transform.position = new Vector3(Input.mousePosition.x,Input.mousePosition.y, 0) ;
	}
	
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D collider){

		damageAmount = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ().fireballDamage;

		collider.gameObject.GetComponent<Enemy> ().hp -= damageAmount;
	}
}
