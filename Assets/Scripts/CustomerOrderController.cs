using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerOrderController : MonoBehaviour
{
    [SerializeField] private Coffees coffees;
    [SerializeField] private GameObject orderIndicator;

    private Order order = new Order();
    private bool isClicked = false;
    public static Action<Order> OnOrderMade;
    public Guid CustomerGUID;

    private void Awake()
    {
        isClicked = false;
    }

    public void ShowUI()
    {
        orderIndicator.SetActive(true);
    }

    public void HideUI()
    {
        orderIndicator.SetActive(false);
    }

    public void RandomizeCoffee()
    {
        order.OrderedCoffee = coffees.GetRandomCoffee();
    }

    public bool IsClicked()
    {
        return isClicked;
    }

    public void MakeOrder()
    {
        isClicked = true;
        CustomerGUID = Guid.NewGuid();
        order.CustomerName = "Mark";
        order.OrderIdentfier = CustomerGUID;
        OnOrderMade?.Invoke(order);
    }
}
