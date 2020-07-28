using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OrderCommand : Command
{

    //TODO: Add order to complete here

    public OrderCommand()
    {
        
    }

    public override async Task Execute()
    {       
        await Task.Delay(50);
    }
}
