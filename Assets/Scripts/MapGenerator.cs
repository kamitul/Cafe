using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CafeType
{
    BASIC,
    ADVANCED
}


public class MapGenerator : MonoBehaviour
{
    private IBuilder builder;
    public Tilemap tilemap;
    public int Width, Height;
    public MapMatrix MapMatrix;
    public List<TileBase> go;

    private async void Awake()
    {
        Task thr = new Task(() => BuildMap(CafeType.BASIC));
        thr.Start();
        await thr;

        LoadMap();
    }

    public void BuildMap(CafeType cafeType)
    {
        switch(cafeType)
        {
            case CafeType.BASIC:
                builder = new BasicCafe(tilemap, Width, Height);
                builder.BuildBackground();
                MapMatrix = builder.GetProduct();
                break;
            default:
                break;
        }
    }

    public void LoadMap()
    {
        for(int i = 0; i< MapMatrix.BoxTiles.Count; ++i)
        {
            for(int j = 0; j < MapMatrix.BoxTiles[i].Count; ++j)
            {
                var elem = MapMatrix.BoxTiles[i][j];
                tilemap.SetTile(elem.Position, go[elem.Id]);
            }
        }
    }
}
