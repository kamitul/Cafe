using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Config;
using Controllers;

namespace UI
{
    public class Order : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;

        private const string ButtonTextPart = " for ";
        public Coffee OrderedCoffee { get; set; }
        public string CustomerName { get; set; }
        public Guid OrderIdentfier { get; set; }

        public void SetButtonText()
        {
            buttonText.text = GetCoffeeName(OrderedCoffee.CoffeeType.ToString()) + ButtonTextPart + CustomerName;
        }

        public static string GetCoffeeName(string coffeeTypeString)
        {
            return coffeeTypeString.Contains("_") ? coffeeTypeString.Replace("_", " ") : coffeeTypeString;
        }

        public void OnOrderButtonClick(CoffeeMakingController coffeeMakingController)
        {
            coffeeMakingController.Initialize(this);
            GetComponent<Button>().interactable = false;
        }
    }
}