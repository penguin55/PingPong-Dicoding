using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Skill skill;
    private float skillDuration;
    [HideInInspector] public bool activateSkill;

    [Header("Skill Bar")] 
    [SerializeField] private Slider skillBar;
    
    private List<Skill> skills = new List<Skill>();

    [Header("Skill Image")] 
    [SerializeField] private Image[] skillImage;
    [SerializeField] private Image skillActiveImage;

    void Update()
    {
        if (!activateSkill) return;
        if (skillDuration > 0)
        {
            skillDuration -= Time.deltaTime;
        }
        else
        {
            activateSkill = false;
            skillDuration = 0;
            skill.NormalCondition();
            skillActiveImage.enabled = false;
            skill = null;
        }
        skillBar.value = skillDuration;
    }

    public void ActivateSkill()
    {
        if (!activateSkill && skills.Count > 0)
        {
            Skill activeSkill = skills[0];
            skills.RemoveAt(0);
            skill = activeSkill;
            skill.UseSkill();
            activateSkill = true;
            skillDuration = skill.durability;
            skillBar.maxValue = skillDuration;
            skillBar.value = skillDuration;
            skillActiveImage.enabled = true;
            skillActiveImage.sprite = skillImage[0].sprite;
            UpdateImage();
        }
    }
    
    public void AddSkill(Skill _skill)
    {
        skills.Insert(0,_skill);
        UpdateImage();
        if (skills.Count == 3) skills.RemoveAt(skills.Count);
    }

    void UpdateImage()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < skills.Count)
            {
                skillImage[i].enabled = true;
                skillImage[i].sprite = skills[i].image;
            } else skillImage[i].enabled = false;
        }
    }
}
