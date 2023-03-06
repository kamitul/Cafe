using System.Collections.Generic;

namespace Map
{
    [System.Serializable]
    public class MapRow
    {
        public List<BoxTile> RowTiles;
        public MapRow(List<BoxTile> tiles)
        {
            RowTiles = tiles;
        }
    }
}