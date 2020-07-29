using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/MultipleTile")]
public class MultipleTile : TileBase
{
    public Sprite[] sprite;
    public bool Collideable = false;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite[Random.Range(0, sprite.Length)];

        if (Collideable)
            tileData.colliderType = Tile.ColliderType.Sprite;
    }
}
