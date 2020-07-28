using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TickableContainter
{
    private List<ITickable> tickables;

    public TickableContainter(List<ITickable> list)
    {
        this.tickables = list;
    }

    public ITickable this[int index]
    {
        get => tickables[index];
    }

    public int Count { get => tickables.Count;  }

    public T Get<T>()
        where T : class, ITickable
    {
        return (tickables.Find(x => x.GetType() == typeof(T)) as T);
    }
}


public class CustomerController : MonoBehaviour
{
    [SerializeField]
    private TickableContainter tickables;

    private void Awake()
    {
        tickables = new TickableContainter(GetComponents<ITickable>().ToList());

        //Add commands
        //GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(tickables.Get<CustomerMovementController>(), new Vector3(18, 5, 0)));
        //GetComponent<CommandInvoker>().AddCommand(new SitCommand(tickables.Get<CustomerMovementController>(), 3));
        //GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(tickables.Get<CustomerMovementController>(), new Vector3(4, 5, 0)));
        //GetComponent<CommandInvoker>().AddCommand(new SitCommand(tickables.Get<CustomerMovementController>(), 5));
    }

    private void Start()
    {
        GetComponent<CommandInvoker>().Run();
    }

    private void Update()
    {
        for(int i =0; i < tickables.Count; ++i)
        {
            tickables[i].Tick();
        }
    }
}
