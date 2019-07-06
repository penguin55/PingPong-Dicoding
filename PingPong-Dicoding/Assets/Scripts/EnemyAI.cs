using UnityEngine;

public class EnemyAI : CharacterBehaviour
{

    [Header("Delay Skill")]
    [SerializeField, Range(0,1)] private float delaySkillMin;
    [SerializeField, Range(1,4)] private float delaySkillMax;
    [SerializeField] private float delay;

    private float hitPosPaddle = 0;
    private float offsetLookingBall;
    private bool expertMode;

    private float min, max;
    
    // Start is called before the first frame update
    void Start()
    {
        expertMode = GameVariable._mode.Equals("AIExpert");
        if (expertMode) offsetLookingBall = -(GameManagement.instance.mapSize.x/2f) + (GameManagement.instance.mapSize.x/6f);
        else offsetLookingBall = 0;
        delay = RandomDelaySkill();
        Debug.Log(delay+" j");
    }
    
    void Update()
    {
        if (GameManagement.instance.freeze) return;
        SearchingOfBallCoordinate();
        UseSkill();
    }

    // Searching the Coordinate of the ball, so the AI can follow the direction of the ball
    void SearchingOfBallCoordinate()
    {
        Vector2 ballPos = GameManagement.instance.ball.transform.position;
        if (LookAtBall(ballPos))
        {
            RestrictToBall(ballPos);
            RestrictVerticalMovement();
        }
        else
        {
            min = -offsetPaddle + (offsetPaddle / 3f);
            max = offsetPaddle - (offsetPaddle / 3f);
            if (expertMode) hitPosPaddle = Random.Range( min , max );
            Debug.Log(hitPosPaddle);
        }
    }

    // Look at the ball x coordinate and determine when the AI should move looking for y coordinate
    // to follow the direction of the ball
    bool LookAtBall(Vector2 positionOfBall)
    {
        return positionOfBall.x >= offsetLookingBall;
    }

    // Restrict to ball y coordinate, so the AI will not move out from the direction of the ball
    void RestrictToBall(Vector2 positionOfBall)
    {
        Vector2 decideDirection = new Vector2(transform.position.x, positionOfBall.y + hitPosPaddle);
        transform.position = Vector2.MoveTowards(transform.position, decideDirection, speed * Time.deltaTime);
    }

    // To Randomly give the delay of using skill
    float RandomDelaySkill()
    {
        return Random.Range(delaySkillMin, delaySkillMax);
    }

    void UseSkill()
    {
        if (skillManager.CanUseSkill() && !skillManager.activateSkill)
        {
            if (delay < 0)
            {
                delay = RandomDelaySkill();
                skillManager.ActivateSkill();
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
    }

}
