using Controllers;
using UnityEngine;

namespace UI
{
    public class IngredientButtonWrapper : MonoBehaviour
    {
        [SerializeField] private CoffeeMakingController coffeeMaker;
        [SerializeField] private Ingredient ingredient;
        public void InvokeEvent()
        {
            coffeeMaker.AddIngredient(ingredient);
        }
    }
}