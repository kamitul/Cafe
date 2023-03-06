using Config;
using System;
using UI;
using UnityEngine;

namespace Controllers
{
    public class CustomerOrderController : Controller
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
        public string Name;

        public OrderInfo OrderInfo { get => orderInfo; }

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
            orderInfo = new OrderInfo(coffees.GetRandomCoffee(), null, Guid.NewGuid());
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
            if (spriteRenderer)
                spriteRenderer.color = isCorrect ? Color.green : Color.red;
        }
    }
}