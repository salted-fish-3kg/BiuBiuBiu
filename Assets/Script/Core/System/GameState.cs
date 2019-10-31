using Knight.Core;
using Knight.Tools.Timer;
using Knight.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus
{
    private GameStatus() { }
    public static GameStatus Instance
    {
        get
        {
            if (_GameStatus ==null)
            {
                _GameStatus = new GameStatus();
            }
            return _GameStatus;
        }
    }
    private static GameStatus _GameStatus;

    public enum Gamestate {
        start,
        playing,
        pause
    }
    Gamestate _gamestate;

    public Gamestate GameState
    {
        get
        {
            return _gamestate;
        }

        set
        {
            _gamestate = value;
        }
    }

    public void GameStart()
    {
        //if (GameState == Gamestate.start) return;
        Debug.Log("初始化开始");
        Debug.Log("加载" + Timer.Instance.name);
        Debug.Log("加载" + UIManager.Instance.name);
        UIManager.Instance.LoadUIPanel("StartPanel", new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.Log("加载" + SceneLoader.Instance);
        Message.AttachObseverEvent("GameStart", StartButtonEvent);
        _gamestate = Gamestate.start;
    }
    private void StartButtonEvent(object[] data)
    {
        Debug.Log("GameStart");
        SceneLoader.Instance.LoadScene("test");
        UIManager.HidePanel("StartPanel");
        UIManager.Instance.LoadUIPanel("LoadPanel", new Vector2(Screen.width / 2, Screen.height / 2));
    }
}

