using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
