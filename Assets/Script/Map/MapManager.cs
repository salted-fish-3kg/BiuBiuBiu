using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField]
    public MapGenerater mapGenerater;
    public List<MapSprite> mapSprites;
    // Start is called before the first frame update
    [SerializeField]
    int heigth;
    [SerializeField]
    int width;
    private SpriteRenderer[,] maps;
    [SerializeField]
    public Vector2 spriteScale;
    [SerializeField]
    public Vector2 offset;
    BlockType[,] blockmap;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    [ContextMenu("GenerateMap")]
    public void DrawGenerateButton()
    {
        if (maps == null) maps = new SpriteRenderer[width, heigth];
        Vector2 cameraPos = Camera.main.transform.position;
        blockmap = mapGenerater.GenerateMap(width, heigth,5);
        for (int i = 0; i < heigth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                SpriteRenderer _sprite = maps[i, j];
                if (_sprite == null)
                {
                    _sprite = CreatSpriteObject(ChoiceSprite(blockmap[i, j]));
                }
                else _sprite.sprite = ChoiceSprite(blockmap[i, j]);
                _sprite.transform.localScale = 5 * Vector2.one;
                _sprite.transform.localPosition =new Vector2(offset.x * j, offset.y * i);
                maps[i, j] = _sprite;
            }
        }
    }
    [ContextMenu("DrawMap")]
    public void DrawMap()
    {
        blockmap = mapGenerater.ObscureMapBlock(blockmap);
        StartCoroutine(SetMaps(blockmap));
    }
    
    IEnumerator SetMaps(BlockType[,] blocks)
    {
        for (int i = 0; i < maps.GetLength(0); i++)
        {
            for (int j = 0; j < maps.GetLength(1); j++)
            {
               if(maps[i, j].sprite == ChoiceSprite(blocks[i, j]))continue;
                maps[i, j].sprite = ChoiceSprite(blocks[i, j]);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    [ContextMenu("ClearMap")]
    public void ClearMap()
    {
        do
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        } while (transform.childCount != 0);
    }
    private Sprite ChoiceSprite(BlockType block)
    {
        switch (block)
        {
            case BlockType.floor:
                return mapSprites[0].icon;
            default:
                return mapSprites[1].icon;
        }
    }
    private SpriteRenderer CreatSpriteObject(Sprite sprite)
    {
        GameObject _obj = new GameObject();
        _obj.transform.SetParent(transform);
        SpriteRenderer renderer = _obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        return renderer;
    }
    [ContextMenu("LoadMapConfigFile")]
    private void LoadMapConfigFile()
    {
        string data = null;
        Knight.Tools.Tools.FileRead(Application.dataPath + "/Config/MapSpriteConfig.txt", ref data);
        Debug.Log(data);
        JsonData json = JsonMapper.ToObject(data);
        Debug.Log(json);
        mapSprites = new List<MapSprite>();
        for (int i = 0; i < json.Count; i++)
        {
            JsonData item = json[i];
            string name = (string)item["name"];
            string address = "Texture/" + (string)item["address"];
            Texture2D texture = Resources.Load<Texture2D>(address);
            string json_rect = (string)item["Rect"];
            Rect rect = JsonMapper.ToObject<Rect>(json_rect);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);
            mapSprites.Add(new MapSprite() { name = name, icon = sprite });
        }
    }
}
public struct MapUnit
{
    string spriteName;
    Vector3 vector3;
}
public enum BlockType
{
    wall,
    floor
}
[Serializable]
public class MapSprite
{
    [SerializeField]
    public Sprite icon;
    [SerializeField]
    public string name;
    [SerializeField]
    public BlockType blockType;
}