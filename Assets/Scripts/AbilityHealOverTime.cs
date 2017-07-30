using System;
using UnityEngine;

namespace Abilities
{
	public class AbilityHealOverTime : Ability
    {

        public override AbilityType type { get { return AbilityType.HealOverTime; } }

		SkillManager skillManager;


		public override void activate(GameObject witch){
			skillManager = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ();

			if(witch.GetComponent<Witch> ().areSoulsEnough (skillManager.healOverTimeCost)){
				witch.GetComponent<Witch> ().Souls -= skillManager.healOverTimeCost;
				GameObject.Instantiate (skillManager.HealOverTimeObject);
			}
		}
			
	}
}

