using Knight.Core;
using Knight.GameController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Knight.CameraControl
{
    public class CameraControler : MonoSingleton<CameraControler>
    {
        Transform characterTF;
        Transform cameraTF;
        Vector3 mousePos;
        Camera mainCamera;
        protected override void Initialization()
        {
            if (characterTF == null) characterTF = GameCore.Instance.Player;
           
            Message.AttachObseverEvent("GameStart", InitCharacter);
            Debug.Log(233);
            mainCamera = Camera.main;
            cameraTF = mainCamera.transform;
        }

        private void InitCharacter(object[] data)
        {
            Debug.Log(GameCore.Instance.Player);
            characterTF = GameCore.Instance.Player;
        }

        // Start is called before the first frame update
        // Update is called once per frame
        void Update()
        {
            if (characterTF == null) return;
            Debug.Log("1");
            if (Input.GetMouseButton(0))
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 temp = mousePos - characterTF.position;
                temp = Vector3.Lerp(Vector3.zero, temp, 0.3f);
                temp.z = -10;
                cameraTF.position =characterTF.position+temp;
            }
        }
    }
}