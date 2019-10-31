using Knight.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Slider))]
public class SliderEditot :Editor
{
    SerializedProperty min;
    SerializedProperty max;
    Slider slider;
    SerializedProperty val;
    RectTransform fill;
    RectTransform slider_rect;
    SerializedProperty fill_image;
    SerializedProperty back_image;
    private void OnEnable()
    {
        slider = target as Slider;
        min = serializedObject.FindProperty("min");
        max = serializedObject.FindProperty("max");
        val = serializedObject.FindProperty("_value");
        fill_image = serializedObject.FindProperty("fill");
        back_image = serializedObject.FindProperty("backGround");
        fill = slider.fill.GetComponent<RectTransform>();
        slider_rect = slider.GetComponent<RectTransform>();
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.ObjectField(fill_image);
        EditorGUILayout.ObjectField(back_image);
        min.floatValue = EditorGUILayout.FloatField("min",min.floatValue);
        max.floatValue = EditorGUILayout.FloatField("max",max.floatValue);
        val.floatValue = EditorGUILayout.Slider("Value", val.floatValue, min.floatValue, max.floatValue);
        serializedObject.ApplyModifiedProperties();
        SetFill(val.floatValue);
    }
    private void SetFill(float value)
    {
        Debug.Log(slider_rect.rect.width * (val.floatValue / (max.floatValue - min.floatValue)));
        fill.sizeDelta = new Vector2(slider_rect.rect.width*(val.floatValue/(max.floatValue-min.floatValue))-slider_rect.rect.width,0);
    }

}
