using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/ResizePlayer", fileName = "ResizePlayer")]
public class ResizePlayer : Skill
{
    [HideInInspector] public GameObject player; 

    private CharacterBehaviour controlPlayer;
    private BoxCollider2D colliderPaddle;

    private float offsetCollider; // This is difference value from size x collider between centerPaddle scale

    private float offsetPaddle; // This is temporary to store original value of offsetPaddle of CharacterBehaviour

    private Vector2 topPaddlePos;
    private Vector2 bottomPaddlePos;
    private Vector2 scalePaddle;
    
    public AudioClip skillSFX;
    public AudioClip normalSFX;

    // To duplicate this scriptable object, to avoid sharing data and changing value between another scriptable
    // object at similar prefabs such as ResizePlaye x 2 and ResizePlayer x 4. They both have same scriptable object
    // and it will sharing the same resource
    public override Skill MakesDuplicate() // Return type of skill because i want to make it polymorphisme in Skill Manager
    {
        ResizePlayer resizePlayer = new ResizePlayer();
        resizePlayer.multiplier = multiplier;
        resizePlayer.durability = durability;
        resizePlayer.image = image;
        resizePlayer.colorSprite = colorSprite;
        resizePlayer.skillSFX = skillSFX;
        resizePlayer.normalSFX = normalSFX;
        return resizePlayer;
    }

    // To initial what is player to be effected
    public override void SetObject(GameObject node)
    {
        player = node;
    }

    // When the player use skill
    public override void UseSkill()
    {
        RecordNormalCondition();
        AudioManager.instance.PlayOneShot(skillSFX);
        SkillEffect();
    }

    // To Record Normal Condition, this method is for changing back normal condition after skill durability is run out
    public void RecordNormalCondition()
    {
        controlPlayer = player.GetComponentInParent<CharacterBehaviour>();
        colliderPaddle = player.GetComponentInChildren<BoxCollider2D>();
        topPaddlePos = controlPlayer.topPaddle.localPosition;
        scalePaddle = controlPlayer.centerPaddle.localScale;
        bottomPaddlePos = controlPlayer.bottomPaddle.localPosition;
        offsetCollider = colliderPaddle.size.x - scalePaddle.x;
        offsetPaddle = controlPlayer.offsetPaddle;
    }

    // The effect of skill
    private void SkillEffect()
    {
        controlPlayer.centerPaddle.localScale = new Vector2(scalePaddle.x * multiplier, 1);
        
        controlPlayer.topPaddle.localPosition = new Vector2(controlPlayer.centerPaddle.localScale.x,0);
        controlPlayer.bottomPaddle.localPosition = new Vector2(-controlPlayer.centerPaddle.localScale.x,0);

        float scaleXCollider = controlPlayer.centerPaddle.localScale.x + offsetCollider;
        colliderPaddle.size = new Vector2(scaleXCollider, colliderPaddle.size.y);
        controlPlayer.offsetPaddle = offsetPaddle + (0.3f * (multiplier - 1));
    }

    // Back to normal condition, before using this skill
    public override void NormalCondition()
    {
        AudioManager.instance.PlayOneShot(normalSFX);
        
        controlPlayer.topPaddle.localPosition = topPaddlePos;
        controlPlayer.centerPaddle.localScale = scalePaddle;
        controlPlayer.bottomPaddle.localPosition = bottomPaddlePos;
        controlPlayer.offsetPaddle = offsetPaddle;
        float scaleXCollider = scalePaddle.x + offsetCollider;
        colliderPaddle.size = new Vector2(scaleXCollider, colliderPaddle.size.y);
    }
}