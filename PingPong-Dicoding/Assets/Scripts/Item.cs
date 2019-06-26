using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Skill skill;

    public enum SkillEffect {Player, Ball, Wall};
    [SerializeField] private SkillEffect effect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!other.GetComponent<Ball>().currentPlayer) return;
            
            if (effect == SkillEffect.Player) skill.SetObject(other.GetComponent<Ball>().currentPlayer);
            else if (effect == SkillEffect.Ball) skill.SetObject(other.gameObject);
            else;

            other.GetComponent<Ball>().currentPlayer.GetComponentInParent<CharacterBehaviour>().skillManager.AddSkill(skill);
            Destroy(gameObject);
        }
    }
}
