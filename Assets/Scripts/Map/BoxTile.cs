using System.Collections.Generic;
using UnityEngine;

namespace Map
{
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
}