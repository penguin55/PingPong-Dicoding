using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [HideInInspector] public GameObject currentPlayer;
    private Rigidbody2D rbd;

    [SerializeField] private float baseForce; //Normal Force
    [HideInInspector] private float force; //Force manipulation in here
    public float skillForce = 1;
    
    private Vector2 direction;

    private Vector2 baseScale;
    
    public void Initialize()
    {
        rbd = GetComponent<Rigidbody2D>();
        direction = new Vector2(2,0).normalized;
        force = baseForce;
        rbd.AddForce(force*direction);
        RecordNormalCondition();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            float sudut =(transform.position.y - other.transform.position.y)*5f;
            direction = new Vector2(rbd.velocity.x, sudut).normalized;
            rbd.velocity = new Vector2(0, 0);
            rbd.AddForce(force*skillForce*direction*2);
            currentPlayer = other.gameObject;
        }

        if (other.gameObject.CompareTag("LeftWall"))
        {
            GameManagement.instance.UpdateScore(0,1);
            ResetBall();
            direction = new Vector2(-2,0).normalized;
            rbd.AddForce(force*skillForce*direction);
        }
        
        if (other.gameObject.CompareTag("RightWall"))
        {
            GameManagement.instance.UpdateScore(1,0);
            ResetBall();
            direction = new Vector2(2,0).normalized;
            rbd.AddForce(force*skillForce*direction);          
        }
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rbd.velocity = new Vector2(0, 0);
        currentPlayer = null;
    }

    void RecordNormalCondition()
    {
        baseScale = transform.localScale;
    }

    public void NormalCondition()
    {
        transform.localScale = baseScale;
        force = baseForce;
        skillForce = 1;
    }

    public void StopBall()
    {
        force = 0;
        rbd.velocity = new Vector2(0, 0);
    }
}
