using System;
using UnityEngine;

namespace Abilities
{
	public abstract class Ability
	{

        public abstract AbilityType type { get; }

		public abstract void activate(GameObject witch);
	}

    public enum AbilityType
    {
        Fireball,
        HealOverTime
    }
}

