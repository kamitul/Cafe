﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee", menuName = "ScriptableObjects/Coffee", order = 2)]

public class Coffee : ScriptableObject
{
    public CoffeeType CoffeeType;
    public List<CoffeeParts> IngredientsToMakeCoffee;

    public int GetWholeAmountOfIngredients()
    {
        int wholeAmount = 0;
        foreach (var ingredient in IngredientsToMakeCoffee)
        {
            wholeAmount += ingredient.IngredientAmount;
        }
        return wholeAmount;
    }
}

[Serializable]
public struct CoffeeParts
{
    public IngredientType IngredientType;
    public int IngredientAmount;
    public Sprite Sprite;
}