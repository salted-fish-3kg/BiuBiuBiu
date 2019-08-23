using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SkillData
{
    [SerializeField]
    string name;
    public string GetName
    {
        get { return name; }
    }
    [SerializeField]
    int damage;
    [SerializeField]
    float coolTime;
    public float GetCoolTime
    {
        get { return coolTime; }
    }
    public bool isCool=true;
    [SerializeField]
    string _selectFunc;
    public string SelectFunc
    {
        get { return _selectFunc; }
    }
    [SerializeField]
    string[] _effectFunc;
    public string[] EffectFunc
    {
        get { return _effectFunc; }
    }
    [SerializeField]
    public GameObject skillPrefab;
    public GameObject onwer;
    public string[] targets;
    [HideInInspector]
    public Vector3 direction;
}
