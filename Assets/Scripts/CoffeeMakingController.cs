using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMakingController : MonoBehaviour
{
    public static Action<Order> OnProperCoffePrepared;
    public static Action<Order> OnWrongCoffePrepared;
    public static Action<Order> OnOrderDelete;

    [SerializeField] private IngredientSpotsController ingredientSpotsController;
    private List<IngredientType> mixedIngredients = new List<IngredientType>();
    private Order order;
    
    public void Initialize(Order _order)
    {
        this.order = _order;
        int wholeAmountOfIngredients = 0;

        foreach (var coffeePart in order.OrderedCoffee.IngredientsToMakeCoffee)
        {
            wholeAmountOfIngredients += coffeePart.IngredientAmount;
        }
        ingredientSpotsController.InitializeIngredientSpots(wholeAmountOfIngredients);
        ingredientSpotsController.InitializeIngredientSpotsText(Order.GetCoffeeName(order.OrderedCoffee.CoffeeType.ToString()));
    }

    public void AddIngredient(Ingredient ingredient)
    {
        mixedIngredients.Add(ingredient.IngredientType);
        ingredientSpotsController.DisplayIngredientOnSpot(ingredient.IngredientSprite);
        Debug.Log(string.Format("Ingredient {0} added to mixing device ", ingredient.IngredientType.ToString()));
    }

    public void ValidatePreparedCoffee()
    {
        if (mixedIngredients != null)
        {
            List<IngredientType> properIngredients = new List<IngredientType>();
            foreach (var coffeePart in order.OrderedCoffee.IngredientsToMakeCoffee)
            {
                for (int i = 0; i < coffeePart.IngredientAmount; i++)
                {
                    properIngredients.Add(coffeePart.IngredientType);
                }
            }

            if (ListEqualier.UnorderedEqual(properIngredients, mixedIngredients))
            {
                OnProperCoffePrepared?.Invoke(order);
                Debug.Log("U prepared proper coffee");
            }
            else
            {
                OnWrongCoffePrepared?.Invoke(order);
                Debug.Log("U Fucked up , try again");
            }
            OnOrderDelete.Invoke(order);
            ResetCoffeeMakeController();
        }
        else
        {
            Debug.Log(string.Format("Try to mix some ingredients in order to make coffee"));
        }
    }

    public void ResetCoffeeMakeController()
    {
        mixedIngredients.Clear();
        order = null;
        ingredientSpotsController.ResetIngredientSpots();
    }
}
