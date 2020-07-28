﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToCommand : Command
{
    private CustomerMovementController movementController;
    private BoxTile current;
    private BoxTile dest;

    public MoveToCommand(CustomerMovementController moveController,BoxTile current, BoxTile destination)
    {
        this.movementController = moveController;
        this.dest = destination;
        this.current = current;
    }

    public override async Task Execute()
    {
        current.Status = TileStatus.AVAILABLE;
        movementController.MoveToPoint((Vector3)dest.Position * 1.28f + new Vector3(1.28f / 2, 1.28f, 0));

        while(movementController.GetDistance() > 0.2f)
        {
            if(token.IsCancellationRequested)
                throw new TaskCanceledException();

            await Task.Delay(50);
        }

        dest.Status = TileStatus.OCCUPIED;
        movementController.Stop();
    }
}
