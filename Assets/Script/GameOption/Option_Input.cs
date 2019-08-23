using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.GameController;
namespace Knight.Option
{
    public class Option_Input : MonoSingleton<Option_Input>
    {
        public Action<Vector3> move;
        public Action<Vector3> direction;
        public Action<bool> fire_1;
        public Action<bool> fire_2;
        public Action<bool> fire_3;
        // Use this for initialization
        Transform player;
        void Start()
        {
            player = GameCore.Instance.Player;
        }

        // Update is called once per frame
        void Update()
        {
            if (move != null) move(GetMoveInput());
            if (direction != null) direction(GetDirection());
            if (fire_1 != null) fire_1(Fire());
        }


        void InputMode()
        {
#if UNITY_ANDROID
            Debug.Log("安卓");
   
#endif
#if UNITY_STANDALONE_WIN
#endif
        }
#if UNITY_STANDALONE_WIN
        private Vector3 GetMoveInput()
        {
            Vector3 vector3 = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) vector3 += Vector3.up;
            if (Input.GetKey(KeyCode.A)) vector3 += Vector3.left;
            if (Input.GetKey(KeyCode.S)) vector3 += Vector3.down;
            if (Input.GetKey(KeyCode.D)) vector3 += Vector3.right;
            return vector3;
        }
        private Vector3 GetDirection()
        {
            return Input.mousePosition - Camera.main.WorldToScreenPoint(player.position);
        }
        private bool Fire()
        {
            return Input.GetMouseButton(0);
        }
#endif
#if UNITY_ANDROID
        private Vector3 GetMoveInput()
        {
            Vector3 vector3 = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) vector3+=Vector3.up;
            if (Input.GetKey(KeyCode.A)) vector3+=Vector3.left;
            if (Input.GetKey(KeyCode.S)) vector3+=Vector3.down;
            if (Input.GetKey(KeyCode.D)) vector3+=Vector3.right;
            return vector3;
   
#endif
    }
}