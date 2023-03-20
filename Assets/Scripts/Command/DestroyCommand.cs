using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commands
{
    public class DestroyCommand : Command
    {
        private readonly List<GameObject> customers;
        private readonly int delay;

        public DestroyCommand(Controller[] controllers, List<GameObject> customers, int delay) : base(controllers)
        {
            this.delay = delay;
            this.customers = customers;
        }

        public override async Task Execute()
        {
            CustomerDestroyerController customerDestroyer = controllers.Find(x => x.GetType() == typeof(CustomerDestroyerController)) as CustomerDestroyerController;
            await Task.Delay(delay * 1000);
            customers.Remove(customerDestroyer.gameObject);
            customerDestroyer.Destroy();
        }
    }
}