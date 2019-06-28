using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterBehaviour
{
    [Header("Controller")] 
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;

    [SerializeField] private KeyCode skill;

    void Update()
    {
        if (GameManagement.instance.freeze) return;
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
