using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Skill;

public class RayCastSelect : ISkillSelect
{
    public string[] GetTarget(SkillData data,Transform skillPorjectlie)
    {
        List<string> target = new List<string>();
        RaycastHit2D hit2D = Physics2D.Raycast(skillPorjectlie.position, -skillPorjectlie.forward, 0.2f);
        if (hit2D.transform.tag == "Enemy") target.Add(hit2D.transform.name);
        return target.ToArray();
    }
}
