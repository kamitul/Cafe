using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicCafe : IBuilder
{
    private Tilemap tilemap;
    private int width;
    private int height;
    private MapMatrix mapMatrix;

    public BasicCafe(Tilemap tilemap, int width, int height)
    {
        this.tilemap = tilemap;
        this.width = width;
        this.height = height;
    }

    public void BuildBackground()
    {
        BoxTile[][] tiles = new BoxTile[width][];
        for (int i = 0; i < tiles.Length; ++i)
        {
            tiles[i] = new BoxTile[height];
        }

        for (int i = 0; i < width; ++i)
        {
            tiles[i][0] = new BoxTile(new Vector3Int(i, 0, 0), 0);
            tiles[i][height - 1] = new BoxTile(new Vector3Int(i, height - 1, 0), 0);
        }

        for (int i = 0; i < height; ++i)
        {
            tiles[0][i] = new BoxTile(new Vector3Int(0, i, 0), 0);
            tiles[width - 1][i] = new BoxTile(new Vector3Int(width - 1, i, 0), 0);
        }

        for(int i = 1; i < width - 1; ++i)
        {
            for(int j = 1; j < height - 1; ++j)
            {
                tiles[i][j] = new BoxTile(new Vector3Int(i, j, 0), 1);
            }
        }

        mapMatrix = new MapMatrix(tiles);
    }

    public void BuildMisc()
    {
        
    }

    public MapMatrix GetProduct()
    {
        return mapMatrix;
    }
}
