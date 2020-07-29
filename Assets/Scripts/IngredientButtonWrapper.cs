using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientButtonWrapper : MonoBehaviour
{
    [SerializeField]
    private CoffeeMakingController coffeeMaker;
    [SerializeField]
    private Ingredient ingredient;
    public void InvokeEvent()
    {
        coffeeMaker.AddIngredient(ingredient);
    }
}
