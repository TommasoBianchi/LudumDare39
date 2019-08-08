using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IDamageable {

    private Witch witch;

	void Start () 
    {
        witch = FindObjectOfType<Witch>();
	}

    public void Damage(float damage)
    {
        witch.Souls -= (int)damage;
    }
}
