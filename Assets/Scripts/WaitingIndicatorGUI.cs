using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingIndicatorGUI : MonoBehaviour
{
    [SerializeField]
    private GifController gifController;

    private void Start()
    {
        gifController.Play("WaitUIIdle");
    }
}
