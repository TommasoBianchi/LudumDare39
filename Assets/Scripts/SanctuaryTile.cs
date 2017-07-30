using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

public class SanctuaryTile : MonoBehaviour
{

    public GameObject skillPrefab;
    public GameObject player;
    public float activationDistance;
    public Vector2 iconOffset;

    [HideInInspector]
    public Ability[] availableAbilities;

    private List<GameObject> skillIcons = new List<GameObject>();

    private bool isActivable = true;
    private bool isShowing = false;

    void Start()
    {
        availableAbilities = SkillManager.GetRandomAbilities();

        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 localPos = -(availableAbilities.Length - 1) / 2f * iconOffset;
        foreach (var ability in availableAbilities)
        {
            GameObject newSkill = Instantiate(skillPrefab, transform);
            newSkill.GetComponent<SpriteRenderer>().sprite = SkillManager.GetIcon(ability.type);
            newSkill.transform.localPosition = localPos;
            skillIcons.Add(newSkill);
            localPos += iconOffset;

            newSkill.GetComponent<Skill>().ability = ability;
            newSkill.GetComponent<Skill>().sanctuaryTile = this;

            newSkill.SetActive(false);
        }

        // Secret
        char randChar = (char)('A' + Random.Range(0, 27));
        GetComponentInChildren<TextMesh>().text = randChar.ToString();
    }

    void Update()
    {
        if (!isActivable)
            return;

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

    public void Inactivate()
    {
        isActivable = false;
        isShowing = false;
        foreach (var icon in skillIcons)
        {
            icon.SetActive(false);
        }
        GetComponentInChildren<TextMesh>().color = Color.clear;
    }
}
