using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Coffees", menuName = "ScriptableObjects/Coffees", order = 2)]

    public class Coffees : ScriptableObject
    {
        [SerializeField] private List<Coffee> coffes;

        public Coffee GetRandomCoffee()
        {
            System.Random random = new System.Random();
            return coffes[random.Next(coffes.Count)];
        }

        public Coffee GetCoffeeObjetcBasedOnCoffeeType(CoffeeType coffeeType)
        {
            return coffes.Find(coffe => coffe.CoffeeType == coffeeType);
        }
    }
}