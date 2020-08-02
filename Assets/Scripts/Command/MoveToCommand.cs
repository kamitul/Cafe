using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MoveToCommand : Command
{
    private MovementController movementController;
    private List<BoxTile> destTiles;
    private Vector3 offset;

    public MoveToCommand(MovementController moveController, List<BoxTile> destTiles, Vector3 offset)
    {
        this.movementController = moveController;
        this.destTiles = destTiles;
        this.offset = offset;
    }

    public override async Task Execute()
    {
        BoxTile destTile = await FindDestTile();
        movementController.MoveToPoint((Vector3)destTile.Position * 1.28f + offset);

        while (movementController.GetDistance() > Mathf.Epsilon)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

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
            if(destAvTiles.Count > 0)
            {
                destTile = destAvTiles[Random.Range(0, destAvTiles.Count - 1)];
                if (destTile.Status == TileStatus.AVAILABLE)
                {
                    if(!destTile.Id.Contains(15)) //temp fix
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
