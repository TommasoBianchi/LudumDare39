using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class AbilityHealOverTime : Ability
	{

		SkillManager skillManager;

		public AbilityHealOverTime ()
		{
			
		}

		public override void activate(GameObject witch){


			skillManager = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ();


			if(witch.GetComponent<Witch> ().soulCheck (skillManager.healOverTimeCost)){
				witch.GetComponent<Witch> ().souls -= skillManager.healOverTimeCost;
				GameObject.Instantiate (skillManager.HealOverTimeObject);
			}
		}
			
	}
}

