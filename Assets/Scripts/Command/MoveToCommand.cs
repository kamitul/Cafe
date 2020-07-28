using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveToCommand : ICommand
{
    private CustomerMovementController movementController;
    private Vector3 dest;

    public MoveToCommand(CustomerMovementController moveController, Vector3 destination)
    {
        this.movementController = moveController;
        this.dest = destination;
    }

    public override async Task Execute()
    {
        movementController.MoveToPoint(dest);

        while(movementController.GetDistance() > 0.1f)
        {
            await Task.Delay(100);
        }

        movementController.Stop();
    }
}
