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
        private const string BUTTON_TEXT_PART = " for ";

        [SerializeField] private TextMeshProUGUI buttonText;

        public Coffee OrderedCoffee { get; set; }
        public string CustomerName { get; set; }
        public Guid OrderIdentfier { get; set; }

        public void SetButtonText()
        {
            buttonText.text = GetCoffeeName(OrderedCoffee.CoffeeType.ToString()) + BUTTON_TEXT_PART + CustomerName;
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