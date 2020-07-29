using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMakingController : MonoBehaviour
{
    public static Action OnProperCoffePrepared;
    public static Action OnWrongCoffePrepared;
    public static Action<Order> OnOrderDelete;

    [SerializeField] private IngredientSpotsController ingredientSpotsController;
    private List<IngredientType> mixedIngredients = new List<IngredientType>();
    private Order order;
    
    private void Initialize(Order _order)
    {
        this.order = _order;
        int wholeAmountOfIngredients = 0;

        foreach (var coffeePart in order.OrderedCoffee.IngredientsToMakeCoffee)
        {
            wholeAmountOfIngredients += coffeePart.IngredientAmount;
        }
        ingredientSpotsController.InitializeIngredientSpots(wholeAmountOfIngredients);
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
                OnProperCoffePrepared?.Invoke();
                Debug.Log("U prepared proper coffee");
            }
            else
            {
                OnWrongCoffePrepared?.Invoke();
                Debug.Log("U Fucked up , try again");
            }
            OnOrderDelete.Invoke(order);
        }
        else
        {
            Debug.Log(string.Format("Try to mix some ingredients in order to make coffee"));
        }
    }
}
