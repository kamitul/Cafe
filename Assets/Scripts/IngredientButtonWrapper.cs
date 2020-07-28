using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientButtonWrapper : MonoBehaviour
{
    [SerializeField]
    private CoffeeMaker coffeeMaker;
    [SerializeField]
    private IngredientType ingredientType;
    public void InvokeEvent()
    {
        Debug.LogError("xd", this);
        coffeeMaker.AddIngredient(ingredientType);
    }
}
