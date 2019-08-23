using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase{
    public abstract void Move(Vector3 vector3);
    public abstract void Direction(Vector3 vector3);
    public abstract void Fire(bool fire);
    public  List<Skill> skills;
    public  delegate void Skill(bool fire);  
}
