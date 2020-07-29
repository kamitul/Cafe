using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Order : MonoBehaviour
{
    [SerializeField] private TextMeshPro buttonText;

    private const string ButtonTextPart = " for ";
    public Coffee OrderedCoffee { get; set; }
    public string CustomerName { get; set; }
    public Guid OrderIdentfier { get; set; }

    public void SetButtonText(Order order)
    {
        CoffeeType coffeeType = order.OrderedCoffee.CoffeeType;
        string customerName = order.CustomerName;
        buttonText.text = GetCoffeeName(coffeeType.ToString()) + ButtonTextPart + customerName;
    }

    public static string GetCoffeeName(string coffeeTypeString)
    {
        return coffeeTypeString.Contains("_") ? coffeeTypeString : coffeeTypeString.Remove(coffeeTypeString.IndexOf("_"), coffeeTypeString.Length);
    }

    public void OnOrderButtonClick(CoffeeMakingController coffeeMakingController)
    {
        coffeeMakingController.Initialize(this);
    }
}
