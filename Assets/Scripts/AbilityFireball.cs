﻿using System;
using UnityEngine;

namespace Abilities
{
	public class AbilityFireball : Ability
	{

        public override AbilityType type { get { return AbilityType.Fireball; } }

		SkillManager skillManager;


		public override void activate(GameObject witch){

			skillManager = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ();

			if(witch.GetComponent<Witch> ().areSoulsEnough (skillManager.fireballCost)){
				witch.GetComponent<Witch> ().Souls -= skillManager.fireballCost;
				GameObject.Instantiate (skillManager.fireballObject, witch.transform.position, Quaternion.identity);
			}
		}
			
	}
}

