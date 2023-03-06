using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public struct CoffeeParts
    {
        public IngredientType IngredientType;
        public int IngredientAmount;
        public Sprite Sprite;
    }
}