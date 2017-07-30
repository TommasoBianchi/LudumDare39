using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

public class SanctuaryTile : MonoBehaviour {

    public GameObject skillPrefab;
    public GameObject player;
    public float activationDistance;
    public Vector2 iconOffset;

    [HideInInspector]
    public Ability[] availableAbilities;

    private List<GameObject> skillIcons = new List<GameObject>();

    private bool isShowing;

	void Start () 
    {
        availableAbilities = new Ability[2];
        availableAbilities[0] = new AbilityFireball();
        availableAbilities[1] = new AbilityHealOverTime();

        Vector2 localPos = -availableAbilities.Length / 2f * iconOffset;
        foreach (var ability in availableAbilities)
        {
            GameObject newSkill = Instantiate(skillPrefab, transform);
            newSkill.GetComponent<SpriteRenderer>().sprite = SkillManager.GetIcon(ability.type);
            newSkill.transform.localPosition = localPos;
            skillIcons.Add(newSkill);
            localPos += iconOffset;
            newSkill.SetActive(false);
        }
	}
	
	void Update () 
    {
        if (!isShowing && (player.transform.position - transform.position).sqrMagnitude < activationDistance * activationDistance)
        {
            isShowing = true;
            foreach (var icon in skillIcons)
            {
                icon.SetActive(true);
            }
        }

        if (isShowing && (player.transform.position - transform.position).sqrMagnitude > activationDistance * activationDistance)
        {
            isShowing = false;
            foreach (var icon in skillIcons)
            {
                icon.SetActive(false);
            }
        }
	}
}
