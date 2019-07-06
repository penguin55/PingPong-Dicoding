using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public Sprite image;
    public Color colorSprite;
    
    public float durability;
    public float multiplier;
    
    
    public virtual void UseSkill(){}
    public virtual void NormalCondition(){}
    public virtual void SetObject(GameObject node){}
    
    public virtual Skill MakesDuplicate(){return new Skill();} // To makes duplicate of this object
    // I make duplicate, because of the sharing data between scriptable object on my prefabs item
    // I makes change when on runtime, so if i don't duplicate this it will change the value of another scriptable object in other prefabs
}
