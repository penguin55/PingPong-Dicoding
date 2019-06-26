using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/ResizeBall", fileName = "ResizeBall")]
public class ResizeBall : Skill
{
    public GameObject ball;
    
    public override void SetObject(GameObject node)
    {
        ball = node;
    }
    
    public override void UseSkill()
    {
        SkillEffect();
    }

    void SkillEffect()
    {
        
    }
    
}
