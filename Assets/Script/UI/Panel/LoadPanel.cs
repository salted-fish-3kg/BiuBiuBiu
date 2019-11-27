using Knight.Core;
using Knight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : UIPanel
{
    Slider slider;
    private void Awake()
    {
        Transform tf_slider = transform.GetChild(0);
        slider = tf_slider.GetComponent<Slider>();
        Message.AttachObseverEvent("SceneLoad", UpdataLoadProgress);
    }

    private void UpdataLoadProgress(object[] data)
    {
        float progress = (float)data[0];
        slider.Value = progress;
        if (progress==1f)
        {
            UIManager.Instance.HidePanel("LoadPanel");
        }
    }
}
