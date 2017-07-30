using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

public class Skill : MonoBehaviour {

    [HideInInspector]
    public Ability ability;
    [HideInInspector]
    public SanctuaryTile sanctuaryTile;

    private Witch witch;

	void Start () 
    {
        witch = FindObjectOfType<Witch>();
	}

    public void AddAbility(IDragDestination destination)
    {
        string key = destination.GetGameObject().GetComponentInChildren<Text>().text;
        witch.addAbility(key, ability);
        sanctuaryTile.Inactivate();
    }
}
