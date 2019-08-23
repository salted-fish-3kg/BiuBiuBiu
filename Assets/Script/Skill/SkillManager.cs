using Knight.Core;
using Knight.Tools.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Knight.Skill
{
    /// <summary>
    /// 释放技能 
    /// 加载技能
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        [SerializeField]
        private SkillData[] skillData;
        // Use this for initialization
        static Dictionary<string, SkillData> skills = new Dictionary<string, SkillData>();
        void Start()
        {
        }
        public void SkillManagerInit()
        {
            InitSkillsData();
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void RelsaseSkill(string name)
        {
            SkillData _skillData = skills[name];
            if (!_skillData.isCool) return;
            ObjectPool.GetObject(name);
            _skillData.isCool = false;
            Timer.Delayer("Cool" + name, _skillData.GetCoolTime, 1, 0, () => { _skillData.isCool = true;});
            //StartCoroutine(SkillCool(_skillData));
        }
        public void LoadSkill(string name)
        {

        }
        private void InitSkillsData()
        {
            for (int i = 0; i < skillData.Length; i++)
            {
                SkillData _skillData = skillData[i];
                skills.Add(_skillData.GetName, _skillData);
                GameObject _obj = Tools.Tools.LoadPrefab<GameObject>("Prefab/Skill/" + skills[_skillData.GetName].GetName);
                skills[_skillData.GetName].skillPrefab = _obj;
                skills[_skillData.GetName].onwer = gameObject;
                ObjectPool.AddNewObjectPool(_skillData.GetName, _obj, (obj) =>
                 {
                     SkillProjectile projectile = obj.GetComponent<SkillProjectile>();
                     if (projectile == null)
                     {
                         Type type = Type.GetType("Knight.Skill." + _skillData.GetName);
                         projectile = obj.AddComponent(type) as SkillProjectile;
                     }
                     projectile.CurrentSkillData = _skillData;
                 });
            }
        }
    }
}