using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    public MapManager MapManager;
    int width;
    int height;
    BlockType[,] map;
    private void Start()
    {

    }
    private void Update()
    {

    }
    public BlockType[,] GenerateMap(int width, int height, int count)
    {
        this.width = width;
        this.height = height;
        Debug.Log(width + "||" + height);
        map = new BlockType[width, height];
        map = RandomFillMapBlock(map);
        for (int i = 0; i < count; i++)
        {
            ObscureMapBlock(map);
        }
        return map;
    }
    private BlockType[,] RandomFillMapBlock(BlockType[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = (BlockType)Random.Range(0, 2);
                Debug.Log("free" + map[i, j]);
            }
        }
        return map;
    }
    public BlockType[,] ObscureMapBlock(BlockType[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                int count = CheckNeighborwall(i, j);
                if (map[i, j] == BlockType.wall)
                    map[i, j] = count >= 4 ? BlockType.wall : BlockType.floor;
                else
                    map[i, j] = count >= 5 ? BlockType.wall : BlockType.floor;
                Debug.Log(map[i, j]);
            }
        }
        return map;
    }
    private int CheckNeighborwall(int x, int y)
    {

        int count = 0;
        //if (x == 0 || x >= width - 1)
        //{
        //    count += 3;
        //}
        //if (y == 0 || y >= height - 1)
        //{
        //    count += 3;
        //}
        //if (count >= 5) return count;
        if (GetMapBlock(x - 1, y - 1) == BlockType.wall) count++;
        if (GetMapBlock(x, y - 1) == BlockType.wall) count++;
        if (GetMapBlock(x + 1, y - 1) == BlockType.wall) count++;
        if (GetMapBlock(x - 1, y) == BlockType.wall) count++;
        if (GetMapBlock(x + 1, y) == BlockType.wall) count++;
        if (GetMapBlock(x - 1, y + 1) == BlockType.wall) count++;
        if (GetMapBlock(x, y + 1) == BlockType.wall) count++;
        if (GetMapBlock(x + 1, y + 1) == BlockType.wall) count++;

        //for (int i = x - 1; i < x + 1; i++)
        //{
        //    if (i < 0 || i >= width) continue;

        //    for (int j = y - 1; j < y + 1; j++)
        //    {
        //        if (j < 0 || j >= height) continue;
        //        if (map[i, j] == BlockType.wall)
        //        {
        //            count++;
        //            Debug.Log("wall");
        //        }
        //    }
        //}
        //Debug.Log(x + "," + y + "||" + count);
        return count;
    }
    BlockType GetMapBlock(int x, int y)
    {
        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);
        return map[x, y];
    }
}
