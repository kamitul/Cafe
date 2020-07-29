using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToCommand : Command
{
    private CustomerMovementController movementController;
    private BoxTile current;
    private BoxTile dest;
    private Vector3 offset;

    public MoveToCommand(CustomerMovementController moveController,BoxTile current, BoxTile destination, Vector3 offset)
    {
        this.movementController = moveController;
        this.dest = destination;
        this.current = current;
        this.offset = offset;
    }

    public override async Task Execute()
    {
        current.Status = TileStatus.AVAILABLE;
        dest.Status = TileStatus.OCCUPIED;

        movementController.MoveToPoint((Vector3)dest.Position * 1.28f + offset);

        while(movementController.GetDistance() > 0.2f)
        {
            if(token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

        movementController.Stop();
    }
}
