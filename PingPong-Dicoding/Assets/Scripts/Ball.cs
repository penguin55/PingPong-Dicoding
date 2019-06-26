using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [HideInInspector] public GameObject currentPlayer;
    private Rigidbody2D rbd;

    [SerializeField] private float baseForce; //Normal Force
    [HideInInspector] public float force; //Force manipulation in here
    
    private Vector2 direction;

    private Vector2 baseScale;
    
    // Start is called before the first frame update
    void Start()
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
            rbd.AddForce(force*direction*2);
            currentPlayer = other.gameObject;
        }

        if (other.gameObject.CompareTag("LeftWall"))
        {
            ResetBall();
            direction = new Vector2(-2,0).normalized;
            rbd.AddForce(force*direction);
            GameManagement.instance.UpdateScore(0,1);
        }
        
        if (other.gameObject.CompareTag("RightWall"))
        {
            ResetBall();
            direction = new Vector2(2,0).normalized;
            rbd.AddForce(force*direction);
            GameManagement.instance.UpdateScore(1,0);
        }
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rbd.velocity = new Vector2(0, 0);
        currentPlayer = null;
        force = baseForce;
    }

    void RecordNormalCondition()
    {
        baseScale = transform.localScale;
    }

    void NormalCondition()
    {
        transform.localScale = baseScale;
        force = baseForce;
    }
}
