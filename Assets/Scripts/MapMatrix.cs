using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum TileStatus
{
    OCCUPIED,
    AVAILABLE
}

[System.Serializable]
public class BoxTile
{
    public Vector3Int Position;
    public List<int> Id = new List<int>();
    public TileStatus Status;

    public BoxTile(Vector3Int pos, int id)
    {
        Position = pos;
        Id.Add(id);
        Status = TileStatus.AVAILABLE;
    }

    public void AddId(int id)
    {
        Id.Add(id);
    }
}

[System.Serializable]
public class MapRow
{
    public List<BoxTile> RowTiles;
    public MapRow(List<BoxTile> tiles)
    {
        RowTiles = tiles;
    }
}

[System.Serializable]
public class MapMatrix
{
    public List<MapRow> BoxTiles;

    public MapMatrix()
    {
    } 

    public void SetTiles(BoxTile[][] tiles)
    {
        BoxTiles = new List<MapRow>();

        for (int i = 0; i < tiles.Length; i++)
        {
            List<BoxTile> temp = new List<BoxTile>();
            for (int j = 0; j < tiles[i].Length; j++)
            {
                temp.Add(tiles[i][j]);
            }
            BoxTiles.Add(new MapRow(temp));
        }
       
    }

    public Tuple<int,int> GetIndexOf(Vector3Int vector3Int)
    {
        for (int i = 0; i < BoxTiles.Count; i++)
        {
            for (int j = 0; j < BoxTiles[i].RowTiles.Count; j++)
            {
                if (BoxTiles[i].RowTiles[j].Position == vector3Int)
                    return new Tuple<int, int>(i, j);
            }
        }

        return null;
    }

    public List<BoxTile> GetId(int id)
    {
        List<BoxTile> boxTiles = new List<BoxTile>();
        for (int i = 0; i < BoxTiles.Count; i++)
        {
            for (int j = 0; j < BoxTiles[i].RowTiles.Count; j++)
            {
                if (BoxTiles[i].RowTiles[j].Id.Contains(id))
                    boxTiles.Add(BoxTiles[i].RowTiles[j]);
            }
        }

        return boxTiles;
    }
}
