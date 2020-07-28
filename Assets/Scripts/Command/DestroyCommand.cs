using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyCommand : Command
{
    private CustomerDestroyer customerDestroyer;
    private List<GameObject> customers;
    private int delay;

    public DestroyCommand(CustomerDestroyer customerDestroyer, List<GameObject> customers, int delay)
    {
        this.customerDestroyer = customerDestroyer;
        this.delay = delay;
        this.customers = customers;
    }

    public override async Task Execute()
    {
        await Task.Delay(delay * 1000);
        customers.Remove(customerDestroyer.gameObject);
        customerDestroyer.Destroy();
    }
}
