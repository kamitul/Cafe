using System.Linq;
using UnityEngine;

namespace UI
{
    public class TickableController : MonoBehaviour
    {
        [SerializeField] private TickableContainter tickables;

        private void Awake()
        {
            tickables = new TickableContainter(GetComponents<ITickable>().ToList());
        }

        private void Update()
        {
            for (int i = 0; i < tickables.Count; ++i)
            {
                tickables[i].Tick();
            }
        }
    }
}