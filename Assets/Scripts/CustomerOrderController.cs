using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerOrderController : MonoBehaviour
{
    [SerializeField] private Coffees coffees;
    [SerializeField] private GameObject orderIndicator;

    private OrderInfo orderInfo = new OrderInfo();
    private bool isClicked = false;
    public static Action<OrderInfo> OnOrderMade;
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
        orderInfo = new OrderInfo();
        orderInfo.OrderedCoffee = coffees.GetRandomCoffee();
    }

    public bool IsClicked()
    {
        return isClicked;
    }

    public void MakeOrder()
    {
        isClicked = true;
        CustomerGUID = Guid.NewGuid();
        orderInfo.CustomerName = "Mark";
        orderInfo.OrderIdentfier = CustomerGUID;
        OnOrderMade?.Invoke(orderInfo);
    }
}
