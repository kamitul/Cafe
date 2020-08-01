using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMakingController : MonoBehaviour
{
    public static Action<Order> OnProperCoffePrepared;
    public static Action<Order> OnWrongCoffePrepared;
    public static Action<OrderInfo> OnOrderDelete;

    [SerializeField] private IngredientSpotsController ingredientSpotsController;
    [SerializeField] private HintsController hintsController;
    private List<IngredientType> mixedIngredients = new List<IngredientType>();
    public Order Order;
    private int currentIngredientsOnCanvas = 0;

    public bool IsMakingOrder { get; set; }

    public void Initialize(Order _order)
    {
        Order = _order;
        int wholeAmountOfIngredients = 0;
        IsMakingOrder = true;

        foreach (var coffeePart in Order.OrderedCoffee.IngredientsToMakeCoffee)
        {
            wholeAmountOfIngredients += coffeePart.IngredientAmount;
        }
        ingredientSpotsController.InitializeIngredientSpots(wholeAmountOfIngredients);
        hintsController.InitializeHintsController(Order);
        ingredientSpotsController.InitializeIngredientSpotsText(Order.GetCoffeeName(Order.OrderedCoffee.CoffeeType.ToString()));
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (currentIngredientsOnCanvas < mixedIngredients.Count)
        {
            mixedIngredients.Add(ingredient.IngredientType);
            ingredientSpotsController.DisplayIngredientOnSpot(ingredient.IngredientSprite);
            Debug.Log(string.Format("Ingredient {0} added to mixing device ", ingredient.IngredientType.ToString()));
            currentIngredientsOnCanvas++;
        }
    }

    public void ValidatePreparedCoffee()
    {
        if (mixedIngredients != null)
        {
            List<IngredientType> properIngredients = new List<IngredientType>();
            foreach (var coffeePart in Order.OrderedCoffee.IngredientsToMakeCoffee)
            {
                for (int i = 0; i < coffeePart.IngredientAmount; i++)
                {
                    properIngredients.Add(coffeePart.IngredientType);
                }
            }

            if (ListEqualier.UnorderedEqual(properIngredients, mixedIngredients))
            {
                OnProperCoffePrepared?.Invoke(Order);
                Debug.Log("U prepared proper coffee");
            }
            else
            {
                OnWrongCoffePrepared?.Invoke(Order);
                Debug.Log("U Fucked up , try again");
            }
            IsMakingOrder = false;
            OrderInfo orderToDelete = new OrderInfo(Order.OrderedCoffee,Order.CustomerName,Order.OrderIdentfier);
            OnOrderDelete.Invoke(orderToDelete);
        }
        else
        {
            Debug.Log(string.Format("Try to mix some ingredients in order to make coffee"));
        }
    }

    public void ResetCoffeeMakeController()
    {
        mixedIngredients.Clear();
        Order = null;
        IsMakingOrder = false;
        currentIngredientsOnCanvas = 0;
        ingredientSpotsController.ResetIngredientSpots();
        hintsController.ResetHintsController();
    }
}
