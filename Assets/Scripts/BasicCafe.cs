using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BasicCafe : IBuilder
{
    private Tilemap tilemap;
    private int width;
    private int height;
    private MapMatrix mapMatrix;
    private BoxTile[][] tiles;

    public BasicCafe(Tilemap tilemap, int width, int height)
    {
        this.tilemap = tilemap;
        this.width = width;
        this.height = height;
        this.mapMatrix = new MapMatrix();
    }

    public void BuildBackground()
    {
        var engine = new System.Random();
        int rand = 0;

        tiles = new BoxTile[width][];
        for (int i = 0; i < tiles.Length; ++i)
        {
            tiles[i] = new BoxTile[height];
        }

        CreateFloor();
        CreateDimensions();
        CreateEnviroment(engine, rand);
        CreateWindows();
        CreateDoor(engine);

        mapMatrix.SetTiles(tiles);
    }

    private void CreateFloor()
    {
        for (int i = 1; i < width - 1; ++i)
        {
            for (int j = 1; j < height - 1; ++j)
            {
                tiles[i][j] = new BoxTile(new Vector3Int(i, j, 0), 3);
            }
        }
    }

    private int CreateDoor(System.Random engine)
    {
        int rand = engine.Next(2, width - 2);
        tiles[rand][0] = new BoxTile(new Vector3Int(rand, 0, 0), 5);
        return rand;
    }

    private void CreateWindows()
    {
        for (int i = 2; i < width; ++i)
        {
            if (i % 5 == 0)
            {
                tiles[i][0] = new BoxTile(new Vector3Int(i, 0, 0), 4);
                tiles[i][height - 1] = new BoxTile(new Vector3Int(i, height - 1, 0), 4);
            }
        }
    }

    private int CreateEnviroment(System.Random engine, int rand)
    {
        for (int i = 0; i < 2; i++)
        {
            rand = engine.Next(2, width - 1);
            tiles[rand][height - 1].AddId(6);
        }

        return rand;
    }

    private void CreateDimensions()
    {
        for (int i = 0; i < width; ++i)
        {
            tiles[i][0] = new BoxTile(new Vector3Int(i, 0, 0), 0);
            tiles[i][height - 1] = new BoxTile(new Vector3Int(i, height - 1, 0), 0);
        }

        for (int i = 0; i < height; ++i)
        {
            tiles[0][i] = new BoxTile(new Vector3Int(0, i, 0), 1);
            tiles[width - 1][i] = new BoxTile(new Vector3Int(width - 1, i, 0), 2);
        }
    }

    public void BuildMisc()
    {
        CreateTables();
        CreateBar();

        mapMatrix.SetTiles(tiles);

    }

    private void CreateBar()
    {
        Vector3Int barStart = new Vector3Int(width - 5, 1, 0);

        for (; barStart.y < 7; barStart.y++)
        {
            tiles[barStart.x][barStart.y].AddId(9);
        }

        Vector3Int barEnd = new Vector3Int(barStart.x + 1, barStart.y - 1, 0);

        for (; barEnd.x < width - 1; barEnd.x++)
        {
            tiles[barEnd.x][barEnd.y].AddId(8);
        }
    }

    private void CreateTables()
    {
        Vector3Int leftTop = new Vector3Int(1, height - 2, 0);
        Vector3Int rightDown = new Vector3Int(width - 8, 2, 0);
        Vector3Int rightTop = new Vector3Int(rightDown.x, leftTop.y, 0);
        Vector3Int leftDown = new Vector3Int(leftTop.x, rightDown.y, 0);

        Vector3Int center = new Vector3Int(rightDown.x + leftTop.x, leftTop.y + rightDown.y, leftTop.z + rightDown.z) / 2;

        CreateTable((center + leftTop) / 2);
        CreateTable((center + leftDown) / 2);
        CreateTable((center + rightTop) / 2);
        CreateTable((center + rightDown) / 2);
    }

    private void CreateTable(Vector3Int offset)
    {
        Tuple<int, int> indexes = mapMatrix.GetIndexOf(offset);
        tiles[indexes.Item1][indexes.Item2].AddId(7);
        CreateStools(offset);
    }

    private void CreateStools(Vector3Int vector3Int)
    {
        Tuple<int, int> indexes = mapMatrix.GetIndexOf(vector3Int + new Vector3Int(1,1, 0));
        tiles[indexes.Item1][indexes.Item2].AddId(10);

        indexes = mapMatrix.GetIndexOf(vector3Int + new Vector3Int(-1, 1, 0));
        tiles[indexes.Item1][indexes.Item2].AddId(10);

        indexes = mapMatrix.GetIndexOf(vector3Int + new Vector3Int(-1, -1, 0));
        tiles[indexes.Item1][indexes.Item2].AddId(10);

        indexes = mapMatrix.GetIndexOf(vector3Int + new Vector3Int(1, -1, 0));
        tiles[indexes.Item1][indexes.Item2].AddId(10);

    }

    public MapMatrix GetProduct()
    {
        return mapMatrix;
    }
}
