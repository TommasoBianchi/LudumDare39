using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

public class SkillManager : MonoBehaviour {

	public Transform HealOverTimeObject;
	public int healOverTimeCost;
	public float healOverTimeTimeBetweenUpdates;
	public float healOverTimeAmount;


	public Transform fireballObject;
	public int fireballCost;
	public int fireballRadius;
	public int fireballDamage;

    public AbilityIcons[] abilityIcons;

    private static Dictionary<AbilityType, Sprite> abilityIconsDictionary = new Dictionary<AbilityType, Sprite>();

    void Start()
    {
        foreach (var icon in abilityIcons)
        {
            abilityIconsDictionary.Add(icon.type, icon.icon);
        }
    }

    public static Sprite GetIcon(AbilityType type)
    {
        return abilityIconsDictionary[type];
    }

    [System.Serializable]
    public struct AbilityIcons
    {
        public AbilityType type;
        public Sprite icon;
    }
}


