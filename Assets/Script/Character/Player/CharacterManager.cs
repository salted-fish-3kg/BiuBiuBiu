using Knight.Core;
using Knight.GameController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using Knight.Skill;

namespace Knight.Character
{
    /// <summary>
    /// 加载角色
    ///     加载预制体
    ///     设置角色控制器
    ///     设置角色属性
    ///     选择角色
    /// </summary>
    public class CharacterManager : Singleton<CharacterManager>
    {
        public enum CharacterName { tank };
        private CharacterControl _CharacterControl;
        public CharacterControl CharacterControl
        {
            get
            {

                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                if (obj == null)
                {
                    obj = new GameObject();
                    obj.name = "Player";
                    obj.tag = "Player";
                    _CharacterControl = obj.AddComponent<CharacterControl>();
                }
                if (_CharacterControl == null) _CharacterControl = obj.GetComponent<CharacterControl>();
                Debug.Log(_CharacterControl);
                return _CharacterControl;

            }
        }
        public override string ToString()
        {
            string[] str = base.ToString().Split('.');
            return str[str.Length - 1];
        }
        private CharacterName _currentCharacter = CharacterName.tank;
        public CharacterName CurrentCharacter
        {
            get { return _currentCharacter; }
        }
        /// <summary>
        /// 加载角色
        /// </summary>
        /// <param name="name"></param>
        public GameObject GetCharacterPrefab(CharacterName name)
        {
            GameObject _character;
            _character = Tools.Tools.LoadPrefab<GameObject>("Prefab/Character/Tank");
            return _character;
        }
        /// <summary>
        /// 设置角色控制器--
        /// </summary>
        private void SetCharacterControl()
        {
            CharacterControl cc = GameCore.Instance.Player.GetComponent<CharacterControl>();
            cc.CharacterControlInit();
        }
        /// <summary>
        /// 设置角色初始属性
        /// </summary>
        private void SetCharacterStatus()
        {

        }
        /// <summary>
        /// 选择角色
        /// </summary>
        /// <param name="name"></param>
        public void ChoiceCharacter(CharacterName name)
        {
            _currentCharacter = name;
            GameObject player = GetCharacterPrefab(name);
            player.transform.SetParent(CharacterControl.transform);
            CharacterControl.enabled = true;
            player.transform.localPosition = Vector3.zero;
            SetCharacterControl();
            SetCharacterStatus();
            SetSkillManager();
        }
        private void SetSkillManager()
        {
            SkillManager skill = GameCore.Instance.Player.GetComponent<SkillManager>();
            skill.SkillManagerInit();
        }
        

       

    }
}