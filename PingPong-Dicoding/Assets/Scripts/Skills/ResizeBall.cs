using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/ResizeBall", fileName = "ResizeBall")]
public class ResizeBall : Skill
{
    [HideInInspector] public GameObject ball;
    
    public AudioClip skillSFX;
    public AudioClip normalSFX;
    
    // To duplicate this scriptable object, to avoid sharing data and changing value between another scriptable
    // object at similar prefabs such as ResizePlaye x 2 and ResizePlayer x 4. They both have same scriptable object
    // and it will sharing the same resource
    public override Skill MakesDuplicate() // Return type of skill because i want to make it polymorphisme in Skill Manager
    {
        ResizeBall resizeBall = new ResizeBall();
        resizeBall.multiplier = multiplier;
        resizeBall.durability = durability;
        resizeBall.image = image;
        resizeBall.colorSprite = colorSprite;
        resizeBall.skillSFX = skillSFX;
        resizeBall.normalSFX = normalSFX;
        return resizeBall;
    }
    
    // to initial the ball variable when ball contact with item prefabs (skill)
    public override void SetObject(GameObject node)
    {
        ball = node;
    }
    
    // This method is accessed when skill is activated
    public override void UseSkill()
    {
        SkillEffect();
    }

    
    // For effect of the skill
    void SkillEffect()
    {
        AudioManager.instance.PlayOneShot(skillSFX);
        ball.transform.localScale *= multiplier;
    }

    
    // Back to normal condition
    public override void NormalCondition()
    {
        AudioManager.instance.PlayOneShot(normalSFX);
        ball.GetComponent<Ball>().UnScaleBall(multiplier);
    }
    
}
