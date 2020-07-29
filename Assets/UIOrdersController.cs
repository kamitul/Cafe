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
    private List<Order> orders;
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

    public void AddOrder(Order order)
    {
        GameObject orderGO = Instantiate(orderElementPrefab, orderSpawnTransform);
        orderGO.SetActive(orders.Count < MaxOrdersOnUIList);
        Order instantiatedOrder = orderGO.GetComponent<Order>();
        Button orderButtonComponent = orderGO.GetComponent<Button>();
        orderButtonComponent.onClick.AddListener(() => instantiatedOrder.OnOrderButtonClick(coffeeMakingController));
        instantiatedOrder.SetButtonText(order);
        orders.Add(Instantiate(orderElementPrefab, orderSpawnTransform).GetComponent<Order>());
    }

    private void DeleteOrder(Order orderToDelete)
    {
        orders.Remove(orderToDelete);
        Destroy(orderToDelete.gameObject);
        orders.Last().gameObject.SetActive(orders.Count < MaxOrdersOnUIList);
        //usuwanie ordera z ui i z listy i aktywowanie poprzednika usuniętego jeżeli jes to możliwe
    }

}
