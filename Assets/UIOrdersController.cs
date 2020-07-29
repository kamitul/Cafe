using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrdersController : MonoBehaviour
{
    [SerializeField] private GameObject orderElementPrefab;
    [SerializeField] private Transform orderSpawnTransform;
    private List<Order> orders;

    private void OnEnable()
    {
        CustomerOrderController.OnOrderMade += AddOrder;
        CoffeeMakingController.OnOrderDelete += DeleteOrder;
    }

    private void OnDisable()
    {
        CustomerOrderController.OnOrderMade -= AddOrder;
    }

    public void AddOrder(Order order)
    {
        //dodawanie ordera do ui i listy
        GameObject orderGO = Instantiate(orderElementPrefab, orderSpawnTransform);
        Order instantiatedOrder = orderGO.GetComponent<Order>();
        instantiatedOrder.InitializeOrder(order);
        orders.Add(Instantiate(orderElementPrefab, orderSpawnTransform).GetComponent<Order>());
    }

    private void DeleteOrder(Order orderToDelete)
    {
        //usuwanie ordera z ui i z listy
    }

}
