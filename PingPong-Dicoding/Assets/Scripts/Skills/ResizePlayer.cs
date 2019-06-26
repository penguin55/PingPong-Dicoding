using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill/ResizePlayer", fileName = "ResizePlayer")]
public class ResizePlayer : Skill
{
    [HideInInspector] public GameObject player;

    private CharacterController controlPlayer;
    private BoxCollider2D colliderPaddle;

    private float offsetCollider; // This is difference value from size x collider between centerPaddle scale

    private float offsetPaddle; // This is temporary to store original value of offsetPaddle of CharacterBehaviour

    private Vector2 topPaddlePos;
    private Vector2 bottomPaddlePos;
    private Vector2 scalePaddle;

    public override void SetObject(GameObject node)
    {
        player = node;
    }

    public override void UseSkill()
    {
        RecordNormalCondition();
        SkillEffect(2);
    }

    public void RecordNormalCondition()
    {
        controlPlayer = player.GetComponentInParent<CharacterController>();
        colliderPaddle = player.GetComponentInChildren<BoxCollider2D>();
        topPaddlePos = controlPlayer.topPaddle.localPosition;
        scalePaddle = controlPlayer.centerPaddle.localScale;
        bottomPaddlePos = controlPlayer.bottomPaddle.localPosition;
        offsetCollider = colliderPaddle.size.x - scalePaddle.x;
        offsetPaddle = controlPlayer.offsetPaddle;
    }

    private void SkillEffect(float multiplier)
    {
        controlPlayer.centerPaddle.localScale = new Vector2(scalePaddle.x * multiplier, 1);
        
        controlPlayer.topPaddle.localPosition = new Vector2(controlPlayer.centerPaddle.localScale.x,0);
        controlPlayer.bottomPaddle.localPosition = new Vector2(-controlPlayer.centerPaddle.localScale.x,0);

        float scaleXCollider = controlPlayer.centerPaddle.localScale.x + offsetCollider;
        colliderPaddle.size = new Vector2(scaleXCollider, colliderPaddle.size.y);
        controlPlayer.offsetPaddle = offsetPaddle + (0.3f * (multiplier - 1));
    }

    public override void NormalCondition()
    {
        controlPlayer.topPaddle.localPosition = topPaddlePos;
        controlPlayer.centerPaddle.localScale = scalePaddle;
        controlPlayer.bottomPaddle.localPosition = bottomPaddlePos;
        controlPlayer.offsetPaddle = offsetPaddle;
        float scaleXCollider = scalePaddle.x + offsetCollider;
        colliderPaddle.size = new Vector2(scaleXCollider, colliderPaddle.size.y);
    }
}