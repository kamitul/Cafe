﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee", menuName = "ScriptableObjects/Coffee", order = 2)]

public class Coffee : ScriptableObject
{
    public CoffeeType CoffeeType;
    public List<CoffeeParts> IngredientsToMakeCoffee;
}

[Serializable]
public struct CoffeeParts
{
    public IngredientType IngredientType;
    public int IngredientAmount;
    public Sprite Sprite;
}