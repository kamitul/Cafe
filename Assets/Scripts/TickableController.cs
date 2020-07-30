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


public class TickableController : MonoBehaviour
{
    [SerializeField]
    private TickableContainter tickables;

    private void Awake()
    {
        tickables = new TickableContainter(GetComponents<ITickable>().ToList());
    }

    private void Update()
    {
        for(int i =0; i < tickables.Count; ++i)
        {
            tickables[i].Tick();
        }
    }
}
