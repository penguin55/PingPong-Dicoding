using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/SlowMotionBall", fileName = "SlowMotionBall")]
public class SlowMotionBall : Skill
{
    [HideInInspector] public GameObject ball;
    
    public AudioClip skillSFX;
    
    // To duplicate this scriptable object, to avoid sharing data and changing value between another scriptable
    // object at similar prefabs such as ResizePlaye x 2 and ResizePlayer x 4. They both have same scriptable object
    // and it will sharing the same resource
    public override Skill MakesDuplicate()// Return type of skill because i want to make it polymorphisme in Skill Manager
    {
        SlowMotionBall slowMotionBall = new SlowMotionBall();
        slowMotionBall.multiplier = multiplier;
        slowMotionBall.durability = durability;
        slowMotionBall.image = image;
        slowMotionBall.colorSprite = colorSprite;
        slowMotionBall.skillSFX = skillSFX;
        return slowMotionBall;
    }
    
    // To initialize what is be effected by this skill
    public override void SetObject(GameObject node)
    {
        ball = node;
    }
    
    public override void UseSkill()
    {
        AudioManager.instance.PlayOneShot(skillSFX);
        SkillEffect();
    }

    // For skill effect after using this skill
    void SkillEffect()
    {
        ball.GetComponent<Ball>().skillForce = (multiplier / 10f);
        ball.GetComponent<Rigidbody2D>().velocity /= multiplier * 2;
    }

    // Return back to normal state after the durability of this skill is run out
    public override void NormalCondition()
    {
        ball.GetComponent<Ball>().NormalCondition();
    }
    
}
