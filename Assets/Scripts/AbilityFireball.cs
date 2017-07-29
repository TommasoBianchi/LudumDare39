using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class AbilityFireball : Ability
	{

		SkillManager skillManager;


		public override void activate(GameObject witch){


			skillManager = GameObject.FindGameObjectWithTag ("SkillManager").GetComponent<SkillManager> ();


			if(witch.GetComponent<Witch> ().soulCheck (skillManager.fireballCost)){
				witch.GetComponent<Witch> ().souls -= skillManager.fireballCost;
				GameObject.Instantiate (skillManager.fireballObject);
			}
		}
			
	}
}

