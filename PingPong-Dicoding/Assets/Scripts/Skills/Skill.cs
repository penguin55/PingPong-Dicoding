using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public Sprite image;
    public float durability;
    [HideInInspector] public int multiplier;
    public virtual void UseSkill(){}
    public virtual void NormalCondition(){}
    public virtual void SetObject(GameObject node){}
}
