using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/SpeedUpBall", fileName = "SpeedUpBall")]
public class SpeedUpBall : Skill
{
    [HideInInspector] public GameObject ball;
    
    public AudioClip skillSFX;

    // To duplicate this scriptable object, to avoid sharing data and changing value between another scriptable
    // object at similar prefabs such as ResizePlaye x 2 and ResizePlayer x 4. They both have same scriptable object
    // and it will sharing the same resource
    public override Skill MakesDuplicate() // Return type of skill because i want to make it polymorphisme in Skill Manager
    {
        SpeedUpBall speedUpBall = new SpeedUpBall();
        speedUpBall.multiplier = multiplier;
        speedUpBall.durability = durability;
        speedUpBall.image = image;
        speedUpBall.colorSprite = colorSprite;
        speedUpBall.skillSFX = skillSFX;
        return speedUpBall;
    }
    
    // TO initialize what is be effected by this skill
    public override void SetObject(GameObject node)
    {
        ball = node;
    }
    
    public override void UseSkill()
    {
        AudioManager.instance.PlayOneShot(skillSFX);
        SkillEffect();
    }

    void SkillEffect()
    {
        ball.GetComponent<Ball>().skillForce = multiplier;
        ball.GetComponent<Rigidbody2D>().velocity *= multiplier;
    }

    // Return back to normal state after the durability of this skill is run out
    public override void NormalCondition()
    {
        ball.GetComponent<Ball>().NormalCondition();
    }
}
