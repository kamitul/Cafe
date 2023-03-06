using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class Hint : MonoBehaviour
    {
        public TextMeshProUGUI amountOfIngredientsText;
        public Image hintIngredientImage;

        public void InitializeHint(string amountOfIngredients, Sprite hintIngredientSprite)
        {
            amountOfIngredientsText.text = "x " + amountOfIngredients;
            hintIngredientImage.sprite = hintIngredientSprite;
        }
    }
}