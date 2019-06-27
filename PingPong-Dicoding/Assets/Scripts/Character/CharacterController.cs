using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterBehaviour
{
    [Header("Controller")] 
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;

    [SerializeField] private KeyCode skill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetKey(up)) direction = Vector2.up;
        if (Input.GetKey(down)) direction = Vector2.down;
        if (Input.GetKeyDown(skill)) ActivateSkill();
        
        Move();
        RestrictVerticalMovement();
        
        direction = Vector2.zero;
    }
}
