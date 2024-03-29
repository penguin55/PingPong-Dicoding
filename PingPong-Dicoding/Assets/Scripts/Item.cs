﻿using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Skill initialSkill;
    private Skill runtimeSkill;

    public enum SkillEffect {Player, Ball, Wall};
    [SerializeField] private SkillEffect effect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!other.GetComponent<Ball>().currentPlayer) return;
            
            AudioManager.instance.GetAnItem();
            
            runtimeSkill = initialSkill.MakesDuplicate();
            
            if (effect == SkillEffect.Player) runtimeSkill.SetObject(other.GetComponent<Ball>().currentPlayer);
            else if (effect == SkillEffect.Ball) runtimeSkill.SetObject(other.gameObject);
            else;

            other.GetComponent<Ball>().currentPlayer.GetComponentInParent<CharacterBehaviour>().skillManager.AddSkill(runtimeSkill);
            Destroy(gameObject);
        }
    }
}
