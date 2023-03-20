using Controllers;
using Map;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commands
{
    public class MoveToCommand : Command
    {
        private readonly List<BoxTile> destTiles;
        private readonly Vector3 offset;

        public MoveToCommand(Controller[] controllers, List<BoxTile> destTiles, Vector3 offset) : base(controllers)
        {
            this.destTiles = destTiles;
            this.offset = offset;
        }

        public override async Task Execute()
        {
            BoxTile destTile = await FindDestTile();
            MovementController movementController = controllers.Find(x => x.GetType() == typeof(MovementController)) as MovementController;
            movementController.MoveToPoint((Vector3)destTile.Position * 1.28f + offset);
            while (!token.IsCancellationRequested && 
                movementController.GetDistance() > Mathf.Epsilon)
            {
                await Task.Delay(50);
            }

            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            movementController.StopAt(destTile);
        }

        private async Task<BoxTile> FindDestTile()
        {
            List<BoxTile> destAvTiles = new List<BoxTile>();
            BoxTile destTile;
            while (true)
            {
                await Task.Delay(Random.Range(10, 50));
                destAvTiles = destTiles.FindAll(x => x.Status != TileStatus.OCCUPIED);
                if (destAvTiles.Count > 0)
                {
                    destTile = destAvTiles[Random.Range(0, destAvTiles.Count - 1)];
                    if (destTile.Status == TileStatus.AVAILABLE)
                    {
                        if (!destTile.Id.Contains(15)) //temp fix
                            destTile.Status = TileStatus.OCCUPIED;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return destTile;
        }
    }
}