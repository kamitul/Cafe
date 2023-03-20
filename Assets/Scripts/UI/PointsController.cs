using UnityEngine;
using TMPro;
using UI;

namespace Controllers
{
    public class PointsController : MonoBehaviour
    {
        private const string POINTS_PREFIX = "Points: ";

        [SerializeField] private TextMeshProUGUI pointsText;

        private int points = 0;

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
            pointsText.text = POINTS_PREFIX + points.ToString();
        }
    }
}