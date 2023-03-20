using Controllers;
using UnityEngine;

namespace UI
{
    public class WaitingIndicatorGUI : MonoBehaviour
    {
        [SerializeField] private GifController gifController;

        private void Start()
        {
            gifController.Play("WaitUIIdle");
        }
    }
}