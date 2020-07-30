using System.Collections;
using System.Collections.Generic;
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
        BoxTile destTile = FindDestTile();

        movementController.MoveToPoint((Vector3)destTile.Position * 1.28f + offset);

        while (movementController.GetDistance() > Mathf.Epsilon)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

        movementController.StopAt(destTile);
    }

    private BoxTile FindDestTile()
    {
        var destAvTiles = destTiles.FindAll(x => x.Status != TileStatus.OCCUPIED);
        BoxTile destTile;
        if (destAvTiles.Count > 0)
            destTile = destAvTiles[Random.Range(0, destAvTiles.Count - 1)];
        else
            destTile = destTiles[Random.Range(0, destTiles.Count - 1)];

        destTile.Status = TileStatus.OCCUPIED;
        return destTile;
    }

    private BoxTile FindCurrentTile()
    {
        return null;
    }
}
