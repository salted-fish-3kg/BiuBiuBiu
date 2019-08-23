using Knight.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Core;
namespace Knight.Character.Enemy
{
    /// <summary>
    /// 死亡
    /// 受伤
    /// 复活
    /// 升级
    /// </summary>
    public class Enemy : MonoBehaviour,IRecycleObject
    {
        [SerializeField]
        EnemyStatus status;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetEffect(SkillEffect effect)
        {

        }

        public void RecycleObject()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}