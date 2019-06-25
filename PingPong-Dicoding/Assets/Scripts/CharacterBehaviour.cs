using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Player Properties")] 
    [SerializeField] protected float speed;
    
    protected List<Skill> skills = new List<Skill>();

    protected Vector2 direction = Vector2.zero;

    protected void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void AddSkill(Skill _skill)
    {
        skills.Add(_skill);
        if (skills.Count == 3) skills.RemoveAt(skills.Count());
    }
}
