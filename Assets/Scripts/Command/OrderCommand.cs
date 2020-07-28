using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OrderCommand : Command
{

    //TODO: Add order to complete here
    private CustomerMovementController movementController;
    private BoxTile current;
    private BoxTile dest;

    public OrderCommand(CustomerMovementController customerMovementController, BoxTile current, BoxTile barTile)
    {
        this.movementController = customerMovementController;
        this.current = current;
        this.dest = barTile;
    }

    public override async Task Execute()
    {
        current.Status = TileStatus.AVAILABLE;
        movementController.MoveToPoint((Vector3)dest.Position * 1.28f + new Vector3(-1.28f / 2, 2.56f, 0));

        while (movementController.GetDistance() > 0.2f)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

        dest.Status = TileStatus.OCCUPIED;
        movementController.Stop();
    }
}
