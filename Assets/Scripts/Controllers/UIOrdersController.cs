using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIOrdersController : MonoBehaviour
    {
        [SerializeField] private GameObject orderElementPrefab;
        [SerializeField] private Transform orderSpawnTransform;
        [SerializeField] private CoffeeMakingController coffeeMakingController;

        private readonly List<Order> orders = new List<Order>();
        private const int MaxOrdersOnUIList = 4;

        private void OnEnable()
        {
            CustomerOrderController.OnOrderMade += AddOrder;
            CoffeeMakingController.OnOrderDelete += DeleteOrder;
            CustomerOrderController.OnOrderRevoked += DeleteOrder;
        }

        private void OnDisable()
        {
            CustomerOrderController.OnOrderMade -= AddOrder;
            CoffeeMakingController.OnOrderDelete -= DeleteOrder;
            CustomerOrderController.OnOrderRevoked -= DeleteOrder;
        }

        public void AddOrder(OrderInfo orderInfo)
        {
            GameObject orderGO = Instantiate(orderElementPrefab, orderSpawnTransform);
            Order instantiatedOrder = orderGO.GetComponent<Order>();
            instantiatedOrder.OrderedCoffee = orderInfo.OrderedCoffee;
            instantiatedOrder.CustomerName = orderInfo.CustomerName;
            instantiatedOrder.OrderIdentfier = orderInfo.OrderIdentfier;
            Button orderButtonComponent = orderGO.GetComponent<Button>();

            orders.Add(instantiatedOrder);
            orderGO.SetActive(orders.Count <= MaxOrdersOnUIList);
            instantiatedOrder.SetButtonText();
            orderButtonComponent.interactable = !coffeeMakingController.IsMakingOrder;
            orderButtonComponent.onClick.AddListener(() => instantiatedOrder.OnOrderButtonClick(coffeeMakingController));
            orderButtonComponent.onClick.AddListener(ToggleOrderButtonsOtherThenCurrent);
        }

        private void DeleteOrder(OrderInfo orderInfo)
        {
            if (coffeeMakingController.Order && orderInfo.OrderIdentfier == coffeeMakingController.Order.OrderIdentfier)
            {
                coffeeMakingController.ResetCoffeeMakeController();
            }
            Order orderToDelete = orders.Find(order => order.OrderIdentfier == orderInfo.OrderIdentfier);
            orders.Remove(orderToDelete);
            Destroy(orderToDelete.gameObject);
            if (orders.Count > 0)
            {
                orders.Last().gameObject.SetActive(orders.Count < MaxOrdersOnUIList);
            }
            //ponowna aktywacja buttonów
            ToggleOrderButtonsOtherThenCurrent();
        }

        private void ToggleOrderButtonsOtherThenCurrent()
        {
            foreach (var orderButton in orders)
            {
                orderButton.GetComponent<Button>().interactable = !coffeeMakingController.IsMakingOrder;
            }
        }
    }
}