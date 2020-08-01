using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerOrderController : MonoBehaviour
{
    [SerializeField] private Coffees coffees;
    [SerializeField] private GameObject orderIndicator;
    [SerializeField] private GameObject waitingIndicator;
    [SerializeField] private GameObject doneIndicator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private OrderInfo orderInfo;
    private bool isClicked = false;
    public static Action<OrderInfo> OnOrderMade;
    public static Action<OrderInfo> OnOrderRevoked;
    public Guid CustomerGUID;
    public string Name;

    private void Awake()
    {
        isClicked = false;
    }

    public void ToggleSetOrder(bool flag)
    {
        orderIndicator.SetActive(flag);
    }

    public void ToggleWaitForOrder(bool flag)
    {
        waitingIndicator.SetActive(flag);
    }

    public void ToggleDoneOrder(bool flag)
    {
        doneIndicator.SetActive(flag);
    }

    public void BreakOrder()
    {
        OnOrderRevoked?.Invoke(orderInfo);
    }

    public void RandomizeCoffee()
    {
        CustomerGUID = Guid.NewGuid();
        orderInfo = new OrderInfo(coffees.GetRandomCoffee(),null, CustomerGUID);
    }

    public bool IsClicked()
    {
        return isClicked;
    }

    public void MakeOrder()
    {
        isClicked = true;
        orderInfo.CustomerName = Name;
        //orderInfo.OrderIdentfier = CustomerGUID;
        OnOrderMade.Invoke(orderInfo);
    }

    public void RecieveOrder(bool isCorrect)
    {
        if(spriteRenderer)
            spriteRenderer.color = isCorrect ? Color.green : Color.red;
    }
}
