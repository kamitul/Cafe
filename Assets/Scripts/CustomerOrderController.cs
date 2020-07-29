using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerOrderController : MonoBehaviour
{
    [SerializeField] private Coffees coffees;
    [SerializeField] private GameObject orderIndicator;
    
    private Coffee coffee;
    private bool isClicked = false;
    public static Action<Coffee> OnOrderMade;

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

    public void Randomize()
    {
        coffee = coffees.GetRandomCoffee();
    }

    public bool IsClicked()
    {
        return isClicked;
    }

    public void MakeOrder()
    {
        Debug.Log("XD");
        isClicked = true;
        OnOrderMade?.Invoke(coffee);
    }
}
