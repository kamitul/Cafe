using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoxTile
{
    public Vector3Int Position;
    public List<int> Id = new List<int>();

    public BoxTile(Vector3Int pos, int id)
    {
        Position = pos;
        Id.Add(id);
    }

    public void AddId(int id)
    {
        Id.Add(id);
    }
}

[System.Serializable]
public class MapMatrix
{
    public List<List<BoxTile>> BoxTiles;


    public MapMatrix(BoxTile[][] tiles)
    {
        BoxTiles = new List<List<BoxTile>>();

        for (int i = 0; i < tiles.Length; i++)
        {
            List<BoxTile> temp = new List<BoxTile>();
            for (int j = 0; j < tiles[i].Length; j++)
            {
                temp.Add(tiles[i][j]);
            }
            BoxTiles.Add(temp);
        }
    }
}
