using Knight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Knight.Skill
{
    public class SkillFactory
    {
        /// <summary>
        /// 方法池
        /// </summary>
        private static Dictionary<string, List<object>> funcCache = new Dictionary<string, List<object>>();
        /// <summary>
        /// 创建技能选取方式
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public static ISkillSelect CreatSkillSelect(SkillData skillData)
        {
            string address = "Knight.Skill." + skillData.SelectFunc;

            return CreatInterface<ISkillSelect>(address);

        }
        /// <summary>
        /// 创建 技能效果
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public static ISkillEffect[] CreatSkillEffect(SkillData skillData)
        {
            ISkillEffect[] skillEffects = new ISkillEffect[skillData.EffectFunc.Length];
            for (int i = 0; i < skillData.EffectFunc.Length; i++)
            {
                string address = "Knight.Skill." + skillData.EffectFunc[i];
                skillEffects[i] = CreatInterface<ISkillEffect>(address);
            }
            return skillEffects;
        }
        private static T CreatInterface<T>(string address) where T : class
        {
            if (!funcCache.ContainsKey(address))
            {
                Type type = Type.GetType("address");
                if (type == null) return null;
                funcCache.Add(address, new List<object>());
                funcCache[address].Add(Activator.CreateInstance(type));
            }
            object temp = funcCache[address][0];
            funcCache[address].Remove(0);
            return temp as T;
        }
        /// <summary>
        /// 回收选取方式
        /// </summary>
        /// <param name="calculater"></param>
        /// <param name="skill"></param>
        public static void CollectSelectCalculater(object calculater, SkillData skill)
        {
            string address = "Knight.Skill" + skill.SelectFunc;
            funcCache[address].Add(calculater);
        }
        /// <summary>
        /// 回收效果
        /// </summary>
        /// <param name="calculater"></param>
        /// <param name="skill"></param>
        public static void CollectEffectCalculater(object[] calculater, SkillData skill)
        {
            for (int i = 0; i < skill.EffectFunc.Length; i++)
            {
                string address = "Knight.Skill." + skill.EffectFunc[i];
                funcCache[skill.EffectFunc[i]].Add(calculater[i]);
            }
        }
 
    }
    public interface ISkillSelect
    {
        string[] GetTarget(SkillData data,Transform skillProjectlie);
    }
    public interface ISkillEffect
    {
        void Effect(SkillData data,Transform skillProjectlie);
    }

}