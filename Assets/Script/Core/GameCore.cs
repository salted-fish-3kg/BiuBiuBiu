using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.Character;
using Knight.InputMode;
using Knight.Tools.Timer;
using Knight.UI;
using Knight.Option;
using System;
using Knight.CameraControl;
using Knight.Core;

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
                    Debug.Log(_player);
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
            Debug.Log(GameStatus.Instance);
            GameStatus.Instance.GameStart();
            //    _Initialization();
            //_Initialization();
            DontDestroyOnLoad(gameObject);
        }
        void _Initialization()
        {
            DontDestroyOnLoad(gameObject);
            //if (_player == null) _player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("初始化开始");
            Debug.Log("加载" + Timer.Instance.name);
            Debug.Log("加载" + UIManager.Instance.name);
            UIManager.Instance.LoadUIPanel("StartPanel", new Vector2(Screen.width/2,Screen.height/2));
            Debug.Log("加载" + SceneLoader.Instance);
            //Message.AttachObseverEvent("GameStart", StartButtonEvent);
            //Debug.Log("加载" + Option_Input.Instance.name);
            //Debug.Log("加载" + CharacterManager.Instance);
            // Debug.Log("加载" + CameraControler.Instance.gameObject);
           // Timer.Delayer("GameStartLoad", 0, 100, 0.01f,);

            Pause = false;
#if UNITY_ANDROID
            Debug.Log("初始化开始");
#endif
#if UNITY_STANDALONE_WIN

#endif
        }
        //游戏流程
        //游戏开始

        //private void StartButtonEvent(object[] data)
        //{
        //    Debug.Log("GameStart");
        //    SceneLoader.Instance.LoadScene("test");
        //    UIManager.Instance.HidePanel("StartPanel");
        //    UIManager.Instance.LoadUIPanel("LoadPanel",new Vector2(Screen.width / 2, Screen.height / 2));
        //}
        #region test
        /// <summary>
        /// tesx
        /// </summary>
        private void OnGUI()
        {
            
        }
        #endregion
    }
}