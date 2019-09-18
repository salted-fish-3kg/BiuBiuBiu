using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地图区域管理
/// 储存对应区域内的sprite
/// </summary>
public class MapAreaCtrl : MonoBehaviour
{
    public BlockType[,] mapblock;
    [SerializeField]
    private Vector2Int order;
    SpriteRenderer[,] renderers;
    public Vector2Int Order
    {
        set
        {
            order = value;
            transform.localPosition = new Vector3(MapManager.Instance.offset.x * (order.x - 0.5f) * MapManager.Instance.mapAreaSize.x, MapManager.Instance.offset.y * (order.y - 0.5f) * MapManager.Instance.mapAreaSize.y);
        }
        get { return order; }
    }
    public void Initialized()
    {
        renderers = new SpriteRenderer[MapManager.Instance.mapAreaSize.x, MapManager.Instance.mapAreaSize.y];
        mapblock = new BlockType[MapManager.Instance.mapAreaSize.x, MapManager.Instance.mapAreaSize.y];
        ForeachMapArea(renderers, CreatSpriteObject);
        //StartCoroutine(CreatRenderers());
    }
    /// <summary>
    /// 在maparea内生成spriterendener
    /// </summary>
    /// <returns></returns>
    #region 
    IEnumerator CreatRenderers()
    {
        ForeachMapArea(renderers, CreatSpriteObject);
        yield return new WaitForEndOfFrame();
    }
    private void CreatSpriteObject(SpriteRenderer spriteRenderer, int i, int j)
    {
        GameObject _obj = new GameObject();
        _obj.transform.SetParent(transform);
        renderers[i, j] = _obj.AddComponent<SpriteRenderer>();
        _obj.transform.localPosition = new Vector2(MapManager.Instance.offset.x * j, MapManager.Instance.offset.y * i);
        _obj.transform.localScale = MapManager.Instance.spriteScale;
    }
    #endregion
    /// <summary>
    /// 刷新地图
    /// </summary>
    public void UpdataMap()
    {
        ForeachMapArea(renderers, SetSpriteObject);
        //if (mapblock != null) StartCoroutine(SetSprite());

    }
    /// <summary>
    /// 设置spriterenderer的sprite;
    /// </summary>
    /// <returns></returns>
    #region
    IEnumerator SetSprite()
    {
        ForeachMapArea(renderers, SetSpriteObject);
        yield return new WaitForEndOfFrame();
    }
    private void SetSpriteObject(SpriteRenderer spriteRenderer, int i, int j)
    {
        renderers[i, j].sprite = MapManager.Instance.ChoiceSprite(mapblock[i, j]);
    }
    #endregion
    void ForeachMapArea<T>(T[,] list, Action<T, int, int> func)
    {
        for (int i = 0; i < list.GetLength(0); i++)
        {
            for (int j = 0; j < list.GetLength(1); j++)
            {
                func(list[i, j], i, j);
            }
            //yield return new WaitForFixedUpdate();
        }
    }
    private void Update()
    {
        Debug.DrawLine(transform.TransformPoint(Vector3.zero), transform.TransformPoint(Vector3.one * 20));
    }
}