using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderIndicatorGUI : MonoBehaviour
{
    [SerializeField]
    private CustomerOrderController customerOrder;

    public void OnMouseDown()
    {
        customerOrder.MakeOrder();
    }
}
