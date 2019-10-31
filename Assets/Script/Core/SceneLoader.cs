using Knight.Core;
using Knight.Tools.Timer;
using Knight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private SceneLoader() { }
    private static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null) _instance = new SceneLoader();
            return _instance;
        }
    }
    // Start is called before the first frame update
    AsyncOperation asyncOperation;
    public void LoadScene(string name)
    {
        asyncOperation = SceneManager.LoadSceneAsync(name);
        Message.AttachSubject("SceneLoad");
        Timer.Delayer("SceneLoad", 0, -1, 0.02f, UpdataLoadProgress);
    }

    private void UpdataLoadProgress()
    {
        if (asyncOperation.progress <= 0.95f)
        {
            Message.Notify("SceneLoad", asyncOperation.progress);
            return;
        }
        Message.Notify("SceneLoad", 1f);
        Timer.RemoveDelayer("SceneLoad");
    }
}
