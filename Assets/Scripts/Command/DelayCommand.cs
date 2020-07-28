using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DelayCommand : Command
{
    private CustomerMovementController movementController;
    private int delay;

    public DelayCommand(CustomerMovementController moveController, int delay)
    {
        this.movementController = moveController;
        this.delay = delay;
    }

    public override async Task Execute()
    {
        movementController.Sit();
        await Task.Delay(delay * 1000);      
    }
}
