using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderController : MonoBehaviour
{
    object Cafe;

    public static Action<object> OnOrderMade;

    public void Randomize()
    {
        // GetRandomCoffe()
    }

    public void MakeOrder()
    {
        OnOrderMade.Invoke(Cafe);
    }
}
