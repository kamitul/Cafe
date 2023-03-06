using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
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
}