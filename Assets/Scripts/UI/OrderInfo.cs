using Config;
using System;

namespace UI
{
    public class OrderInfo
    {
        public Coffee OrderedCoffee { get; private set; }
        public string CustomerName { get; set; }
        public Guid OrderIdentfier { get; private set; }

        public OrderInfo(Coffee cofee, string name, Guid identifier)
        {
            OrderedCoffee = cofee;
            CustomerName = name;
            OrderIdentfier = identifier;
        }
    }
}