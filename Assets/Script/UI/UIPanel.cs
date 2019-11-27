using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  显示UIPanel
///  隐藏UIpanel
/// </summary>
namespace Knight.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Display()
        {
            if (gameObject.activeInHierarchy) return;
            gameObject.SetActive(true);
            DisplayAnimation();
            enabled = true;
        }
        public virtual void Hide()
        {
            if (!gameObject.activeInHierarchy) return;
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