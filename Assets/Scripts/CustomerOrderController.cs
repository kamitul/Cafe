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

    private OrderInfo orderInfo = new OrderInfo();
    private bool isClicked = false;
    public static Action<OrderInfo> OnOrderMade;
    public static Action OnOrderRevoked;
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
        OnOrderRevoked?.Invoke();
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
        orderInfo.CustomerName = Name;
        orderInfo.OrderIdentfier = CustomerGUID;
        OnOrderMade?.Invoke(orderInfo);
    }

    public void RecieveOrder(bool isCorrect)
    {
        if(spriteRenderer)
            spriteRenderer.color = isCorrect ? Color.green : Color.red;
    }
}
