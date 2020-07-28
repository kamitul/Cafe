using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    
    //for debug pourpuses public
    public CoffeeType orderedCoffee;

    public List<IngredientType> mixedIngredients;

    [SerializeField] private List<Coffee> Coffees;

    public static Action OnProperCoffePrepared;
    public static Action OnWrongCoffePrepared;
    
    public void AddIngredient(IngredientType ingredientType)
    {
        mixedIngredients.Add(ingredientType);
        Debug.Log(string.Format("Ingredient {0} added to mixing device XD", ingredientType.ToString()));
    }

    public void ValidatePreparedCoffee()
    {
        if (mixedIngredients != null)
        {

            Debug.LogError("xd2", this);
            Coffee coffeeSO = Coffees.Find(coffee => coffee.CoffeeType == orderedCoffee);
            List<IngredientType> properIngredients = new List<IngredientType>();
            foreach (var coffeePart in coffeeSO.IngredientsToMakeCoffee)
            {
                for (int i = 0; i < coffeePart.IngredientAmount; i++)
                {
                    properIngredients.Add(coffeePart.IngredientType);
                    Debug.LogError(coffeePart.IngredientType);
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
