using Knight.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knight.UI
{
    public class Slider : MonoBehaviour
    {
        [SerializeField]
        public Image backGround;
        [SerializeField]
        public Image fill;
        RectTransform rect_backGround;
        RectTransform rect_fill;
        [SerializeField]
        public float min = 0;
        [SerializeField]
        public float max = 1;
        [SerializeField]
        float _value;
        RectTransform rect;
        public float Value
        {
            get { return _value; }
            set
            {
                _value = Mathf.Clamp(value, min, max);
                _Init();
                SetFill(Value * 2);
            }
        }
        void Start()
        {
            _Init();
        }
        void _Init()
        {
            if (rect != null) return; 
            if (backGround == null)
            {
                backGround = transform.FindChildByName("BackGround").GetComponent<Image>();
            }
            rect_backGround = backGround.GetComponent<RectTransform>();
            if (fill == null)
            {
                fill = transform.FindChildByName("Fill").GetComponent<Image>();
            }
            rect_fill = fill.GetComponent<RectTransform>();
            rect = GetComponent<RectTransform>();

        }
        private void SetFill(float value)
        {
            rect_fill.sizeDelta = new Vector2(rect.rect.width * (value / (max - min)-2), 0);
        }
    }
}