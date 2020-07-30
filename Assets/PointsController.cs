using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    
    private int points = 0;
    private string pointsPrefix = "Points: ";

    private void OnEnable()
    {
        CoffeeMakingController.OnProperCoffePrepared += AddPoints;
        CoffeeMakingController.OnWrongCoffePrepared += RemovePoints;
        HintsController.OnShowHint += RemovePoints;
        AssignPointsToText();
    }

    private void OnDisable()
    {
        CoffeeMakingController.OnProperCoffePrepared -= AddPoints;
        CoffeeMakingController.OnWrongCoffePrepared -= RemovePoints;
        HintsController.OnShowHint -= RemovePoints;
    }

    private void RemovePoints(Order obj)
    {
        points--;
        AssignPointsToText();
    }

    private void AddPoints(Order obj)
    {
        points += 3;
        AssignPointsToText();
    }

    private void AssignPointsToText()
    {
        pointsText.text = pointsPrefix + points.ToString();    
    }
}
