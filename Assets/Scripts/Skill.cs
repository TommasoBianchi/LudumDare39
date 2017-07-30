using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

public class Skill : MonoBehaviour {

    public Ability ability;

    private Witch witch;

	void Start () 
    {
        witch = FindObjectOfType<Witch>();
	}

    public void AddAbility(IDragDestination destination)
    {
        string key = destination.GetGameObject().GetComponentInChildren<Text>().text;
    }
}
