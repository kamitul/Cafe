using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientSpotsController : MonoBehaviour
{
    [SerializeField] private GameObject spotPrefab;
    [SerializeField] private Transform spotsSpawnTransform;
    [SerializeField] private TextMeshPro orderText;


    private List<IngredientSpot> spawnedIngredientSpots = new List<IngredientSpot>();
    private int displayedIngredientsAmount = 0;
    public void InitializeIngredientSpots(int orderedCoffeeIngredientsAmount)
    {
        for (int i = 0; i < orderedCoffeeIngredientsAmount; i++)
        {
            spawnedIngredientSpots.Add(Instantiate(spotPrefab, spotsSpawnTransform).GetComponent<IngredientSpot>());
        }
    }

    public void DisplayIngredientOnSpot(Sprite ingredientSpriteToShow)
    {
        spawnedIngredientSpots[displayedIngredientsAmount].IngredientImage.sprite = ingredientSpriteToShow;
        spawnedIngredientSpots[displayedIngredientsAmount].IngredientImage.color = new Color(1,1,1,1);
        displayedIngredientsAmount++;
    }

    public void ClearIngredientsSprites()
    {
        foreach (var ingredientSpot in spawnedIngredientSpots)
        {
            ingredientSpot.IngredientImage.sprite = null;
            ingredientSpot.IngredientImage.color = new Color(1, 1, 1, 0);
        }
        displayedIngredientsAmount = 0;
    }

    public void ResetIngredientSpots()
    {
        foreach (var ingredientSpot in spawnedIngredientSpots)
        {
            Destroy(ingredientSpot.gameObject);
        }
        spawnedIngredientSpots?.Clear();
        displayedIngredientsAmount = 0;
    }
}