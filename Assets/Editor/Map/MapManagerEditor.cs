using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEditorInternal;
using LitJson;
using Knight.Tools;
using System.IO;

[CanEditMultipleObjects]
[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    static ReorderableList m_list;
    SerializedProperty mapSprite;
    SerializedProperty mapGenerate;
    SerializedProperty offset;
    SerializedProperty spriteScale;
    SerializedProperty mapAreaSize;
    SerializedProperty mapSize;
    //SerializedProperty manSize;
    private void Awake()
    {

    }
    private void OnEnable()
    {
        mapSprite = serializedObject.FindProperty("mapSprites");
        mapGenerate = serializedObject.FindProperty("mapGenerater");
        offset = serializedObject.FindProperty("offset");
        spriteScale = serializedObject.FindProperty("spriteScale");
        mapAreaSize = serializedObject.FindProperty("mapAreaSize");
        mapSize = serializedObject.FindProperty("mapSize");
        //mapSize = serializedObject.FindProperty("mapSize");
        CreateReorderableList();
        SetupReordierableHeaderDrawer();
        SetupReordireableListElementDarwer();
    }

    private void CreateReorderableList()
    {
        m_list = new ReorderableList(serializedObject,
                        mapSprite,
                        true, true, true, true);
        m_list.elementHeight = 80;
    }
    private void SetupReordierableHeaderDrawer()
    {
        m_list.drawHeaderCallback =
            (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "MapSprite");
            };
    }
    private void SetupReordireableListElementDarwer()
    {
        m_list.drawElementCallback = (rect, indexer, isActive, isFocuse) =>
        {
            if (mapSprite == null || mapSprite.arraySize == 0)
            {
                return;
            }
            var element = mapSprite.GetArrayElementAtIndex(indexer);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };
    }
    private void SetupReordireableListOnAddCallback()
    {
        m_list.onAddCallback = (ReorderableList list) =>
        {
            if (list == null) ReorderableList.defaultBehaviours.DoAddButton(list);
            list.serializedProperty.arraySize++;
        };

    }
    private void DrawReorderableList()
    {
        m_list.DoLayoutList();
    }
    private void DrawMapSizeConfig()
    {
        //EditorGUILayout.PropertyField(mapSize, true);
        //EditorGUILayout.PropertyField(mapSize, true);
        EditorGUILayout.PropertyField(mapSize);
        EditorGUILayout.PropertyField(mapAreaSize);
        //    GUILayout.TextField("2333");
        //    length.intValue = EditorGUI.IntField(new Rect() { position = _position }, "Length", length.intValue);
        //    width.intValue = EditorGUI.IntField(new Rect() { position = new Vector2(_position.x + 30, _position.y) }, "Width", width.intValue);
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (mapGenerate==null)   EditorGUILayout.PropertyField(mapGenerate);
        EditorGUILayout.PropertyField(spriteScale);
        EditorGUILayout.PropertyField(offset);
        DrawMapSizeConfig();
        GUILayout.Space(20);
        GenerateButton();
        DrawReorderableList();
        serializedObject.ApplyModifiedProperties();

    }
    //[MenuItem("CONTEXT/MapManager/GenerateMap")]
    private void GenerateButton()
    {
        if (GUILayout.Button("SaveMapSpriteInfo")) SaveMapSpriteConfig();
    }
    [MenuItem("CONTEXT/MapManager/SaveMapSpriteConfig")]
    private static void SaveMapSpriteConfig()
    {
        JsonMapper.RegisterImporter<JsonData, Rect>(ImportRect);
        JsonMapper.RegisterExporter<Rect>(ExproterRect);
        SerializedProperty array_mapSize = m_list.serializedProperty;
        JsonData jsonArray = new JsonData();
        for (int i = 0; i < array_mapSize.arraySize; i++)
        {
            SerializedProperty _serObJ = array_mapSize.GetArrayElementAtIndex(i);
            string name = _serObJ.FindPropertyRelative("name").stringValue;
            Sprite icon = (Sprite)_serObJ.FindPropertyRelative("icon").objectReferenceValue;
            Texture texture = icon.texture;
            //SpriteInfo spriteInfo = new SpriteInfo();
            //spriteInfo.textureName = texture.name;
            //spriteInfo.x = (int)icon.rect.x;
            //spriteInfo.y = (int)icon.rect.y;
            //spriteInfo.height = (int)icon.rect.height;
            //spriteInfo.width = (int)icon.rect.width;

            JsonData json = new JsonData();
            json["name"] = name;
            json["address"] = icon.texture.name;
            json["Rect"] = JsonMapper.ToJson(icon.rect);
            jsonArray.Add(json);
        }
        Debug.Log(JsonMapper.ToJson(jsonArray));
        Knight.Tools.Tools.FileWrite(Application.dataPath + "/Config/MapSpriteConfig.txt", JsonMapper.ToJson(jsonArray), FileMode.Create);
        AssetDatabase.Refresh();
    }

    private static void ExproterRect(Rect obj, JsonWriter writer)
    {
        writer.WriteObjectStart();
        writer.WritePropertyName("x");
        writer.Write((int)obj.x);
        writer.WritePropertyName("y");
        writer.Write((int)obj.y);
        writer.WritePropertyName("height");
        writer.Write((int)obj.height);
        writer.WritePropertyName("width");
        writer.Write((int)obj.width);
        writer.WriteObjectEnd();
        Debug.Log(writer);
    }

    private static Rect ImportRect(JsonData data)
    {
        Rect rect = new Rect();
        rect.x = (int)data["x"];
        rect.y = (int)data["y"];
        rect.height = (int)data["height"];
        rect.width = (int)data["width"];
        return rect;
    }

    public class SpriteInfo
    {
        public string textureName;
        public int x;
        public int y;
        public int width;
        public int height;
    }
}
//[CustomPropertyDrawer(typeof(MapSize))]
//class MapSizeEdito : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {

//        Rect lenghtRect = new Rect(position) { };
//        Rect widthRect = new Rect(position) { y = position.y + 20 };
//        SerializedProperty lenght = property.FindPropertyRelative("heigth");
//        SerializedProperty width = property.FindPropertyRelative("width");
//        Vector2Int _vector = new Vector2Int(lenght.intValue, width.intValue);       
//        _vector = EditorGUI.Vector2IntField(position, "Mapsize", _vector);
//        lenght.intValue = _vector.x;
//        width.intValue = _vector.y;
//        Debug.Log("l" + lenght.intValue);
//        Debug.Log("w" + width.intValue);
//    }
//}


