using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DelayCommand : Command
{
    private int delay;

    public DelayCommand(Controller[] controllers, int delay) : base(controllers)
    {
        this.delay = delay;
    }

    public override async Task Execute()
    {
        MovementController movementController = controllers.Find(x => x.GetType() == typeof(MovementController)) as MovementController;
        movementController.Stop();
        await Task.Delay(delay * 1000);      
    }
}
