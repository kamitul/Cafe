using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountOfIngredientsText;
        [SerializeField] private Image hintIngredientImage;

        public void InitializeHint(string amountOfIngredients, Sprite hintIngredientSprite)
        {
            amountOfIngredientsText.text = "x " + amountOfIngredients;
            hintIngredientImage.sprite = hintIngredientSprite;
        }
    }
}