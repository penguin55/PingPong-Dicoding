﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/SlowMotionBall", fileName = "SlowMotionBall")]
public class SlowMotionBall : Skill
{
    [HideInInspector] public GameObject ball;
    
    public override Skill MakesDuplicate()
    {
        Skill resizePlayer = new SlowMotionBall();
        resizePlayer.multiplier = multiplier;
        resizePlayer.durability = durability;
        resizePlayer.image = image;
        return resizePlayer;
    }
    
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
        ball.GetComponent<Ball>().skillForce = (multiplier / 10f);
        ball.GetComponent<Rigidbody2D>().velocity /= multiplier * 2;
    }

    public override void NormalCondition()
    {
        ball.GetComponent<Ball>().NormalCondition();
    }
    
}
