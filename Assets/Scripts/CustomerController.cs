using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField]
    private List<ITickable> tickables;

    private void Awake()
    {
        tickables = GetComponents<ITickable>().ToList();
    }
}
