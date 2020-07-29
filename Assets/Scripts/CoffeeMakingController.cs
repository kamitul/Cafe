using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMakingController : MonoBehaviour
{
    public static Action OnProperCoffePrepared;
    public static Action OnWrongCoffePrepared;

    [SerializeField] private IngredientSpotsController ingredientSpotsController;
    [SerializeField] private List<Coffee> Coffees;
    private List<IngredientType> mixedIngredients = new List<IngredientType>();
    
    [Header("For testing only")]
    public  Coffee orderedCoffee;


    private void Start()
    {
        int wholeAmountOfIngredients = 0;
        foreach (var coffeePart in orderedCoffee.IngredientsToMakeCoffee)
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
            foreach (var coffeePart in orderedCoffee.IngredientsToMakeCoffee)
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
        }
        else
        {
            Debug.Log(string.Format("Try to mix some ingredients in order to make coffee"));
        }
    }
}
