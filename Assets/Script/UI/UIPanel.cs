using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  显示UIPanel
///  隐藏UIpanel
/// </summary>
namespace Knight.UI
{
    public class UIPanel : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
        public virtual void Display()
        {
            if (gameObject.activeInHierarchy) return;
            gameObject.SetActive(true);
            DisplayAnimation();
            enabled = true;
        }
        public virtual void Hide()
        {
            if (gameObject.activeInHierarchy) return;
            HidenAnimation();
            gameObject.SetActive(false);
            enabled = false;
        }
        public bool IsActive
        {
            get { return gameObject.activeInHierarchy; }
        }
        protected virtual void HidenAnimation()
        {
        }
        protected virtual void DisplayAnimation()
        {
        }
    }
}