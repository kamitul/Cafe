using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HintsController : MonoBehaviour
{
    //nahama
    public static Action<Order> OnShowHint;

    [SerializeField] private GameObject hintPrefab;
    [SerializeField] private Transform hintSpawnTransfom;
    [SerializeField] private TextMeshProUGUI hintCoffeText;
    [SerializeField] private GameObject hintButtonUI;
    [SerializeField] private GameObject hintContentUI;


    List<Hint> hints = new List<Hint>();
    Coffee orderedCoffe;

    private void Awake()
    {
        ToggleHintContentUI(false);
        ToggleHintButtonUI(false);
    }

    public void InitializeHintsController(Order order)
    {
        orderedCoffe = order.OrderedCoffee;
        ToggleHintButtonUI(true);
    }

    public void ResetHintsController()
    {
        foreach (var hint in hints)
        {
            Destroy(hint.gameObject);
        }
        hints.Clear();
        orderedCoffe = null;
        ToggleHintButtonUI(false);
        ToggleHintContentUI(false);
    }

    public void InitializeHints()
    {
        hintCoffeText.text = Order.GetCoffeeName(orderedCoffe.CoffeeType.ToString());
        foreach (var ingredient in orderedCoffe.IngredientsToMakeCoffee)
        {
            GameObject hintGO = Instantiate(hintPrefab, hintSpawnTransfom);
            Hint hint = hintGO.GetComponent<Hint>();
            hint.InitializeHint(ingredient.IngredientAmount.ToString(), ingredient.Sprite);
            hints.Add(hint);
        }
        OnShowHint.Invoke(null);
    }

    public void ToggleHintButtonUI(bool enabled)
    {
        hintButtonUI.SetActive(enabled);
    }
    public void ToggleHintContentUI(bool enabled)
    {
        hintContentUI.SetActive(enabled);
    }
}
