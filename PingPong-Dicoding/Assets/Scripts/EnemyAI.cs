using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CharacterBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        SearchingOfBallCoordinate();
    }

    void SearchingOfBallCoordinate()
    {
        Vector2 ballPos = GameManagement.instance.ball.transform.position;
        if (LookAtBall(ballPos))
        {
            direction = new Vector2(0, Mathf.Clamp(ballPos.y, -1, 1));
            RestrictToBall(ballPos);
            RestrictVerticalMovement();
        }
    }

    bool LookAtBall(Vector2 positionOfBall)
    {
        return positionOfBall.x >= 0;
    }

    void RestrictToBall(Vector2 positionOfBall)
    {
        Vector2 decideDirection = new Vector2(transform.position.x, positionOfBall.y);
        transform.position = Vector2.MoveTowards(transform.position, decideDirection, speed * Time.deltaTime);
    }

}
