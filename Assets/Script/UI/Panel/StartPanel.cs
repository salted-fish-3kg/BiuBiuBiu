using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knight.UI;
using UnityEngine.UI;
using Knight.Tools;
using Knight.Core;
using System;

public class StartPanel : UIPanel
{
    Text title;
    Button gameStart;
    Button option;
    Button exit;
    Subject GameStart;
    // Start is called before the first frame update
    void Start()
    {
        Transform _title = transform.FindChildByName("text_gametitle");
        Transform _gameStart = transform.FindChildByName("btn_start");
        Transform _option = transform.FindChildByName("btn_option");
        Transform _exit = transform.FindChildByName("btn_exit");
        title = _title.GetComponent<Text>();
        gameStart = _gameStart.GetComponent<Button>();
        option = _option.GetComponent<Button>();
        exit = _exit.GetComponent<Button>();
        GameStart = Message.AttachSubject("GameStart");
        gameStart.onClick.AddListener(GameStartEvent);
    }
    //private void GameStartEvent()
    //{
    //    Message.Notify("GameStart");
    //}
    private void GameStartEvent()
    {

        Debug.Log("GameStart");
        UIManager.Instance.HidePanel("StartPanel");
        UIManager.Instance.DisplayPanel("CharacterSelectPanel");
        //UIManager.Instance.LoadUIPanel("LoadPanel", new Vector2(Screen.width / 2, Screen.height / 2));

    }
}
