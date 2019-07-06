using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Player Properties")] 
    public SkillManager skillManager;
    public Transform topPaddle;
    public Transform centerPaddle;
    public Transform bottomPaddle;
    [ Range(0,4)] public float offsetPaddle; 
    [SerializeField] protected float speed;
    

    protected Vector2 direction = Vector2.zero;

    // For moving the player on Vertical Axis
    protected void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // To restrict vertical movement, so as not to get out of bounds
    public void RestrictVerticalMovement()
    {
        float directionY = Mathf.Clamp(transform.position.y , -GameManagement.instance.mapSize.y/2f + offsetPaddle, GameManagement.instance.mapSize.y/2f - offsetPaddle);
        float directionX = transform.position.x;
        transform.position = new Vector2(directionX, directionY);
    }

    // To activate a skill
    protected void ActivateSkill()
    {
        skillManager.ActivateSkill();
    }
}
