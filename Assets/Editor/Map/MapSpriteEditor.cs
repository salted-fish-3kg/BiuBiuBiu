using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MapSprite))]
public class MapSpriteEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 60;
            position.height = EditorGUIUtility.singleLineHeight;
            Rect iconRect = new Rect(position) { width = 68, height = 68 };
            Rect nameRect = new Rect(position) { width = position.width - 70, x = position.x + 70 };
            Rect blockRect = new Rect(position) { width = position.width - 70, y = position.y + 20, x = position.x +70};
            SerializedProperty iconProperty = property.FindPropertyRelative("icon");
            SerializedProperty nameProperty = property.FindPropertyRelative("name");
            SerializedProperty blockProperty = property.FindPropertyRelative("blockType");
            iconProperty.objectReferenceValue =
                EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Sprite), false);
            nameProperty.stringValue = EditorGUI.TextField(nameRect, "Name", nameProperty.stringValue);
            EditorGUI.PropertyField(blockRect, blockProperty);
        }
    }
}
