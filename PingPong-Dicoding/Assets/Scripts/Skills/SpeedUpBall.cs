using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/SpeedUpBall", fileName = "SpeedUpBall")]
public class SpeedUpBall : Skill
{
    [HideInInspector] public GameObject ball;

    public override Skill MakesDuplicate()
    {
        Skill resizePlayer = new SpeedUpBall();
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
        ball.GetComponent<Ball>().skillForce = multiplier;
        ball.GetComponent<Rigidbody2D>().velocity *= multiplier;
    }

    public override void NormalCondition()
    {
        ball.GetComponent<Ball>().NormalCondition();
    }
}
