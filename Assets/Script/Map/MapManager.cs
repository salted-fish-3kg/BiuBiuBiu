using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// map
/// </summary>
[Serializable]
public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField]
    public MapGenerater mapGenerater;
    public List<MapSprite> mapSprites;
    public MapAreaCtrl[] mapAreas;
    private BlockType[,] mapblocks;
    // Start is called before the first frame update
    [SerializeField]
    public Vector2Int mapAreaSize;
    [SerializeField]
    public Vector2 spriteScale;
    [SerializeField]
    public Vector2 offset;
    [SerializeField]
    public Vector2Int mapSize;
    bool isOK;
    protected override void Initialization()
    {
        base.Initialization();
        LoadMapConfigFile();
    }
    private void T1()
    {
        Thread thread = new Thread(new ThreadStart(CreatMapBlock));
    }
    /// <summary>
    /// 获取sprite
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    public Sprite ChoiceSprite(BlockType block)
    {
        switch (block)
        {
            case BlockType.floor:
                return mapSprites[0].icon;
            default:
                return mapSprites[1].icon;
        }
    }
    /// <summary>
    /// 生成SpriteRender;
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    private SpriteRenderer CreatSpriteObject(Sprite sprite)
    {
        GameObject _obj = new GameObject();
        _obj.transform.SetParent(transform);
        SpriteRenderer renderer = _obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        return renderer;
    }
    /// <summary>
    /// 加载地图本地配置文件
    /// </summary>
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
    #region 初始化地图
    public void InitMapComponentCoroutine()
    {
        mapGenerater.GenerateMapAsyn(mapSize.x * mapAreaSize.x, mapSize.y * mapAreaSize.y, 5);
        StartCoroutine(InitMapComponent());
    }
    IEnumerator InitMapComponent()
    {
        //CreatMapBlock();
        yield return new WaitUntil(mapGenerater.GenerateState);
        mapblocks = mapGenerater.GetMap();
        CreatMapArea();
        SetMapAreaPos();
        yield break;
    }
    private bool test()
    {
        CreatMapBlock();
        CreatMapArea();
        SetMapAreaPos();
        return true;
    }
    /// <summary>
    /// 初始化MapAreaCtrl
    /// </summary>
    public void CreatMapArea()
    {
        mapAreas = new MapAreaCtrl[9];
        for (int i = 0; i < 9; i++)
        {
            GameObject _obj = new GameObject();
            _obj.name = "mapArea_" + i;
            _obj.transform.SetParent(transform);
            MapAreaCtrl _mapArea = _obj.AddComponent<MapAreaCtrl>();
            mapAreas[i] = _mapArea;
            _mapArea.Initialized();
        }
    }
    private void SetMapAreaPos()
    {
        for (int i = 0; i < 9; i++)
        {
            int y = i / 3;
            int x = i % 3;
            mapAreas[i].Order = new Vector2Int(x - 1, y - 1);
            SetMapAreaBlock(mapAreas[i]);
            mapAreas[i].UpdataMap();
        }
        //transform.position = -Vector3.Lerp(mapAreas[8].transform.position, mapAreas[4].transform.position, 0.5f);
    }
    //private void GetMapBlock()
    //{
    //    mapblocks = mapGenerater.GenerateMap(mapSize.x * mapAreaSize.x, mapSize.y * mapAreaSize.y, 4);
    //}
    private void CreatMapBlock()
    {
        mapblocks = mapGenerater.GenerateMap(mapSize.x * mapAreaSize.x, mapSize.y * mapAreaSize.y, 5);
        isOK = true;
    }
    private bool Task2()
    {
        Debug.Log(isOK);
        return isOK;
    }
    private void SetMapAreaBlock(MapAreaCtrl areaCtrl)
    {
        Vector2Int _pos = areaCtrl.Order;
        _pos = _pos + Vector2Int.one * 4;
        for (int i = 0; i < mapAreaSize.y; i++)
        {
            for (int j = 0; j < mapAreaSize.x; j++)
            {
                areaCtrl.mapblock[i, j] = mapblocks[i + _pos.y * 25, j + _pos.x * 25];
            }
        }
    }
    #endregion
    #region 切换地图区域
    private void MoveRight()
    {
        int _min = mapAreas[0].Order.x;
        for (int i = 0; i < 3; i++)
        {
            if (mapAreas[i].Order.x < _min) _min = mapAreas[i].Order.x;
        }
        for (int i = 0; i < mapAreas.Length; i++)
        {
            if (mapAreas[i].Order.x == _min)
            {
                mapAreas[i].Order = new Vector2Int(mapAreas[i].Order.x + 3, mapAreas[i].Order.y);
                SetMapAreaBlock(mapAreas[i]);
                mapAreas[i].UpdataMap();
            }
        }
    }
    private void MoveLeft()
    {
        int _max = mapAreas[0].Order.x;
        for (int i = 0; i < 3; i++)
        {
            if (mapAreas[i].Order.x > _max) _max = mapAreas[i].Order.x;
        }
        for (int i = 0; i < mapAreas.Length; i++)
        {
            if (mapAreas[i].Order.x == _max)
            {
                if ((mapAreas[i].Order.x - 3) + 4 < 0) return;
                mapAreas[i].Order = new Vector2Int(mapAreas[i].Order.x - 3, mapAreas[i].Order.y);
                SetMapAreaBlock(mapAreas[i]);
                mapAreas[i].UpdataMap();
            }
        }
    }
    private void MoveUp()
    {
        int _min = mapAreas[0].Order.y;
        Debug.Log(_min);
        for (int i = 0; i < 3; i++)
        {
            if (mapAreas[i * 3].Order.y < _min) _min = mapAreas[i * 3].Order.y;
        }
        Debug.Log(_min);
        for (int i = 0; i < mapAreas.Length; i++)
        {
            if (mapAreas[i].Order.y == _min)
            {
                mapAreas[i].Order = new Vector2Int(mapAreas[i].Order.x, mapAreas[i].Order.y + 3);
                SetMapAreaBlock(mapAreas[i]);
                mapAreas[i].UpdataMap();
            }
        }
    }
    private void MoveDown()
    {
        int _max = mapAreas[0].Order.y;
        for (int i = 0; i < 3; i++)
        {
            if (mapAreas[i * 3].Order.y > _max) _max = mapAreas[i * 3].Order.y;
        }
        for (int i = 0; i < mapAreas.Length; i++)
        {
            if (mapAreas[i].Order.y == _max)
            {
                mapAreas[i].Order = new Vector2Int(mapAreas[i].Order.x, mapAreas[i].Order.y - 3);
                SetMapAreaBlock(mapAreas[i]);
                mapAreas[i].UpdataMap();
            }
        }
    }
    #endregion
    private void ChangeMapCenter(MapAreaCtrl mapArea)
    {
        Vector2Int _pos = mapArea.Order + Vector2Int.one * 5;

    }
    bool key_wDown = false;
    bool key_sDown = false;
    bool key_aDown = false;
    bool key_dDown = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && (!key_wDown))
        {
            key_wDown = true;
            Debug.Log("MoveUp");
            MoveUp();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            key_wDown = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && (!key_sDown))
        {
            key_sDown = true;
            Debug.Log("MoveDown");
            MoveDown();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            key_sDown = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && (!key_aDown))
        {
            key_aDown = true;
            Debug.Log("MoveLeft");
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            key_aDown = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && (!key_dDown))
        {
            key_dDown = true;
            Debug.Log("MoveRight");
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            key_dDown = false;
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