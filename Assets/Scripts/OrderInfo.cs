using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo
{
    public Coffee OrderedCoffee;
    public string CustomerName;
    public Guid OrderIdentfier;

    public OrderInfo(Coffee _orderedCoffee, string _customerName, Guid _orderIdentifier)
    {
        OrderedCoffee = _orderedCoffee;
        CustomerName = _customerName;
        OrderIdentfier = _orderIdentifier;
    }
}
