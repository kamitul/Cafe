using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintsController : MonoBehaviour
{
    [SerializeField] private GameObject hintPrefab;
    [SerializeField] private Transform hintSpawnTransfom;
    [SerializeField] private TextMeshProUGUI hintCoffeText;
    [SerializeField] private GameObject hintButtonUI;
    [SerializeField] private GameObject hintContentUI;


    List<Hint> hints = new List<Hint>();
    Coffee orderedCoffe;

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
        orderedCoffe = null;
        ToggleHintButtonUI(true);
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
