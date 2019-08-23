using Knight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Knight.Tools;

public class TipsPanel : UIPanel
{
    Text text_title;
    Text text_tips;
    Button _btn_yes;
    Button _btn_no;
    private Button btn_yes
    {
        get
        {
            if (_btn_yes == null) _btn_yes = transform.FindChildByName("yes").GetComponent<Button>();
            return _btn_yes;
        }
        set
        {
            if (_btn_yes == null) _btn_yes = transform.FindChildByName("yes").GetComponent<Button>();
            _btn_yes = value;
        }
    }
    private Button btn_no
    {
        get
        {
            if (_btn_no == null) _btn_yes = transform.FindChildByName("no").GetComponent<Button>();
            return _btn_no;
        }
        set
        {
            if (_btn_no == null) _btn_yes = transform.FindChildByName("no").GetComponent<Button>();
            _btn_no = value;
        }
    }
    static string text_title_normal = "Warmming";
    static string text_yes_normal = "yes";
    string text_no_normal = "no";
    public string title
    {
        //get
        //{
        //    if (text_title == null)
        //    {
        //        text_title = transform.Find("title").GetComponent<Text>();
        //    }
        //    return text_title.text;
        //}
        set
        {
            if (text_title == null)
            {
                text_title = transform.FindChildByName("title").GetComponent<Text>();
            }
            text_title.text = value;
        }
    }
    public string tips
    {
        //    get
        //    {
        //        if (text_tips == null)
        //        {
        //            text_tips = transform.Find("tips").GetComponent<Text>();
        //        }
        //        return text_tips.text;
        //    }
        set
        {
            if (text_tips == null)
            {
                text_tips = transform.FindChildByName("tips").GetComponent<Text>();
            }
            text_tips.text = value;
        }
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetYesBtn(Action yesToDo, string yesText = "yes")
    {
        btn_yes.onClick.AddListener(
            delegate ()
            {
                if (yesToDo != null) yesToDo();
                UIManager.Instance.HidePanel("TipsPanel");
            });

    }
    public void SetNoBtn(Action noToDo, string yesText = "no")
    {
        btn_no.onClick.AddListener(
            delegate ()
            {
                if (noToDo != null) noToDo();
                UIManager.Instance.HidePanel("TipsPanel");
            });
    }
    public override void Hide()
    {
        base.Hide();

    }

}
