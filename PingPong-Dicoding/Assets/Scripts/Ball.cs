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

    // To initialize a ball for the first level begin
    public void Initialize()
    {
        rbd = GetComponent<Rigidbody2D>();
        float x = Random.Range(0, 10) % 2;
        direction = new Vector2(x == 1 ? 2 : -2,0).normalized;
        force = baseForce;
        rbd.AddForce(force*direction);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayBallCollide("Paddle");
            
            float sudut =(transform.position.y - other.transform.position.y)*5f;
            direction = new Vector2(rbd.velocity.x, sudut).normalized;
            rbd.velocity = new Vector2(0, 0);
            rbd.AddForce(force*skillForce*direction*2);
            currentPlayer = other.gameObject;
        }
        
        if (other.gameObject.CompareTag("Wall")) AudioManager.instance.PlayBallCollide("Wall");

        if (other.gameObject.CompareTag("LeftWall"))
        {
            AudioManager.instance.PlayBallCollide("Dead");
            
            GameManagement.instance.UpdateScore(0,1);
            ResetBall();
            direction = new Vector2(-2,0).normalized;
            rbd.AddForce(force*skillForce*direction);
        }
        
        if (other.gameObject.CompareTag("RightWall"))
        {
            AudioManager.instance.PlayBallCollide("Dead");
            
            GameManagement.instance.UpdateScore(1,0);
            ResetBall();
            direction = new Vector2(2,0).normalized;
            rbd.AddForce(force*skillForce*direction);          
        }
    }

    // To reset the ball if the ball collide left or right wall
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rbd.velocity = new Vector2(0, 0);
        currentPlayer = null;
    }

    // To change back the normal state of the ball
    public void NormalCondition()
    {
        force = baseForce;
        skillForce = 1;
    }

    // UnScale Ball is for unscalling ball from skill effect. This method is accessed from ResizeBall script
    public void UnScaleBall(float scaleMultiplier)
    {
        transform.localScale /= scaleMultiplier;
    }

    // To stop the movement of the ball
    public void StopBall()
    {
        force = 0;
        rbd.velocity = new Vector2(0, 0);
    }
}
