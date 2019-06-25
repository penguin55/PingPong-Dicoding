using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Skill skill;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().currentPlayer.GetComponent<CharacterBehaviour>().AddSkill(skill);
        }
    }
}
