using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveToCommand : Command
{
    private MovementController movementController;
    private List<BoxTile> tiles;
    private Vector3 offset;

    public MoveToCommand(MovementController moveController, List<BoxTile> tiles, Vector3 offset)
    {
        this.movementController = moveController;
        this.tiles = tiles;
        this.offset = offset;
    }

    public override async Task Execute()
    {
        var currentTile = tiles.Find(x => x.Position == (movementController.GetPosition() / 1.28f));
        var destTiles = tiles.FindAll(x => x.Status != TileStatus.OCCUPIED);
        BoxTile destTile;
        if (destTiles.Count > 0)
            destTile = destTiles[Random.Range(0, destTiles.Count - 1)];
        else
            destTile = tiles[Random.Range(0, tiles.Count - 1)];

        if (currentTile != null)
            currentTile.Status = TileStatus.AVAILABLE;
        destTile.Status = TileStatus.OCCUPIED;

        movementController.MoveToPoint((Vector3)destTile.Position * 1.28f + offset);

        while(movementController.GetDistance() > 0.2f)
        {
            if(token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

        movementController.Stop();
    }
}
