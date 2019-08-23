using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制panel显示
/// display all panel
/// 控制panel隐藏
/// hide all panel
/// 切换panel
/// 查看panel当前状态
/// 加载panel预制体
/// 默认动画
/// 警告窗
/// </summary>
namespace Knight.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        TipsPanel Tips;
        Vector2 screenScale;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }
        Dictionary<string, UIPanel> panels;
        /// <summary>
        ///显示UIPanel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DisplayPanel(string name)
        {
            if (!panels.ContainsKey(name)) LoadUIPanel(name, new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (!panels.ContainsKey(name)) return false;
            panels[name].Display();
            if (panels[name].IsActive) return true;
            return false;
        }
        /// <summary>
        /// 隐藏UIPanel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HidePanel(string name)
        {
            if (!panels.ContainsKey(name)) return false;
            panels[name].Hide();
            if (panels[name].IsActive) return false;
            return true;
        }
        /// <summary>
        /// hied all panel
        /// </summary>
        public bool HideAllPanel()
        {
            bool all = true;
            foreach (var item in panels)
            {
                if (!HidePanel(item.Key)) all = false;
            }
            return all;
        }
        /// <summary>
        /// switch panel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SwitchPanel(string name)
        {
            if (!HideAllPanel()) return false;
            if (!DisplayPanel(name)) return false;
            return true;
        }
        /// <summary>
        /// 查看uipanel状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UI_Status(string name)
        {
            if (panels.ContainsKey(name)) return false;
            return panels[name].IsActive;
        }
        public void OpenTips(string title, string tipsText, Action yesTODo, Action noToDo)
        {

        }
        public void OpenTips(string tipsText, Action yesTODo, Action noToDo)
        {
            DisplayPanel("TipsPanel");
            Tips.tips = tipsText;
            Tips.SetYesBtn(yesTODo);
            Tips.SetNoBtn(noToDo);
        }
        public void OpenTips(string tipsText, Action yesTODo, string yesText, Action noToDo, string noText)
        {

        }
        protected override void Initialization()
        {
            screenScale = new Vector2(Screen.width, Screen.height);
            Debug.Log(screenScale);
            base.Initialization();
            panels = new Dictionary<string, UIPanel>();
            transform.SetParent(GameObject.Find("Canvas").transform);
            LoadUIPanel("TipsPanel", screenScale * 0.5f);
            Tips = panels["TipsPanel"] as TipsPanel;
        }
        public void LoadUIPanel(string name, Vector2 position)
        {
            GameObject obj = Resources.Load("UI/" + name) as GameObject;
            obj = Instantiate(obj);
            obj.transform.SetParent(transform.parent);
            UIPanel panel = obj.GetComponent<UIPanel>();
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.position = position;
            if (panel != null) panels.Add(name, panel);
        }
    }
}