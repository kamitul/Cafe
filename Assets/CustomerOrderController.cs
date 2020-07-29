using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderController : MonoBehaviour
{
    [SerializeField] private Coffees coffees;

    private Coffee coffee;

    public static Action<Coffee> OnOrderMade;

    public void Randomize()
    {
        coffee = coffees.GetRandomCoffee();    
    }

    public void MakeOrder()
    {
        OnOrderMade.Invoke(coffee);
    }
}
