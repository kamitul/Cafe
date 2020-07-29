using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIOrdersController : MonoBehaviour
{
    [SerializeField] private GameObject orderElementPrefab;
    [SerializeField] private Transform orderSpawnTransform;
    [SerializeField] private CoffeeMakingController coffeeMakingController;
    private List<Order> orders = new List<Order>();
    private const int MaxOrdersOnUIList = 4;

    private void OnEnable()
    {
        CustomerOrderController.OnOrderMade += AddOrder;
        CoffeeMakingController.OnOrderDelete += DeleteOrder;
    }

    private void OnDisable()
    {
        CustomerOrderController.OnOrderMade -= AddOrder;
        CoffeeMakingController.OnOrderDelete += DeleteOrder;
    }

    public void AddOrder(OrderInfo orderInfo)
    {
        GameObject orderGO = Instantiate(orderElementPrefab, orderSpawnTransform);
        Order instantiatedOrder = orderGO.GetComponent<Order>();
        instantiatedOrder.OrderedCoffee = orderInfo.OrderedCoffee;
        instantiatedOrder.CustomerName = orderInfo.CustomerName;
        instantiatedOrder.OrderIdentfier = orderInfo.OrderIdentfier;
        orders.Add(instantiatedOrder);
        orderGO.SetActive(orders.Count < MaxOrdersOnUIList);
        Button orderButtonComponent = orderGO.GetComponent<Button>();
        instantiatedOrder.SetButtonText();
        orderButtonComponent.onClick.AddListener(() => instantiatedOrder.OnOrderButtonClick(coffeeMakingController));
    }

    private void DeleteOrder(Order orderToDelete)
    {
        orders.Remove(orderToDelete);
        Destroy(orderToDelete.gameObject);
        if (orders.Count > 0)
        {
            orders.Last().gameObject.SetActive(orders.Count < MaxOrdersOnUIList);
        }
        //usuwanie ordera z ui i z listy i aktywowanie poprzednika usuniętego jeżeli jes to możliwe
    }

}
