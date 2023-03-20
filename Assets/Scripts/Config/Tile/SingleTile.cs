using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Tilemaps.Tile;

namespace Config
{
    [CreateAssetMenu(menuName = "Assets/SingleTile")]
    public class SingleTile : TileBase
    {
        public Sprite sprite;
        public bool Collideable = false;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;

            if (Collideable)
                tileData.colliderType = ColliderType.Sprite;
        }
    }
}