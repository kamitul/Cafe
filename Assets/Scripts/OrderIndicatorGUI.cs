using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderIndicatorGUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CustomerOrderController customerOrder;

    public void OnMouseDown()
    {
        customerOrder.MakeOrder();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        customerOrder.MakeOrder();
    }
}
