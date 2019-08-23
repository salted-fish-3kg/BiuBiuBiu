using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Character;
using Knight.InputMode;
using Knight.Tools.Timer;
using Knight.UI;
using Knight.Option;
using System;

namespace Knight.GameController
{
    public class GameCore : MonoSingleton<GameCore>
    {
        public bool gameBegin = false;
        public GameObject Core
        {
            get
            {
                if (_core == null) _core = GameObject.Find("Core");
                return _core;
            }
        }
        GameObject _core;
        Transform _player;
        public Transform Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player").transform;
                }
                return _player;
            }
        }
        private bool _pause;
        public bool Pause
        {
            set
            {
                if (value == false)
                {
                    if (PauseEvetn == null) return;
                    PauseEvetn();
                }
                if (value == true)
                {
                    if (ContinueEvent == null) return;
                    ContinueEvent();
                }
                _pause = value;
            }
        }
        public Action ContinueEvent;
        public Action PauseEvetn;
        protected override void Initialization()
        {
            base.Initialization();
            //    _Initialization();
            _Initialization();
        }
        void _Initialization()
        {
            if (_player == null) _player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("初始化开始");
            Debug.Log("加载" + Timer.Instance.name);
            Debug.Log("加载" + UIManager.Instance.name);
            Debug.Log("加载" + Option_Input.Instance.name);
            Debug.Log("加载角色");
            Debug.Log("加载" + CharacterManager.Instance);
            Pause = false;
#if UNITY_ANDROID
            Debug.Log("初始化开始");
#endif
#if UNITY_STANDALONE_WIN

#endif

        }
    }
}