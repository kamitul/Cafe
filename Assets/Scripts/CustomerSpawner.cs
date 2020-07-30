using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> customers;

    [SerializeField]
    private MapGenerator mapGenerator;

    [SerializeField]
    private GameObject customerPrefab;

    public float SpawnRate = 7f;
    public int MaxCustomers = 7;
    public float MaxOrderMakeTime = 3f;
    public float MaxOrderGetTime = 6f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if(customers.Count < MaxCustomers && timer > SpawnRate)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        var go = Instantiate(customerPrefab, mapGenerator.GetTile(5)[0].Position + new Vector3Int(2, 2, 0), Quaternion.identity, null);
        go.GetComponent<MovementController>().StopAt(mapGenerator.GetTile(5)[0]);
        PrepareCommands(go);

        customers.Add(go);
    }

    private void PrepareCommands(GameObject go)
    {
        var door = mapGenerator.GetTile(5);
        var stools = mapGenerator.GetTile(10);
        var bars = mapGenerator.GetTile(8).Concat(mapGenerator.GetTile(9)).ToList();

        CommandInvoker invoker = go.GetComponent<CommandInvoker>();
        invoker.AddCommand(new MoveToCommand(go.GetComponent<MovementController>(), stools, new Vector3(1.28f / 2, 1.28f, 0)));
        invoker.AddCommand(new DelayCommand(go.GetComponent<MovementController>(), UnityEngine.Random.Range(5,20)));
        invoker.AddCommand(new MoveToCommand(go.GetComponent<MovementController>(), bars, new Vector3(-1.28f / 2, 2.28f, 0)));
        invoker.AddCommand(new OrderCommand(go.GetComponent<CustomerOrderController>(), MaxOrderMakeTime, MaxOrderGetTime));
        invoker.AddCommand(new MoveToCommand(go.GetComponent<MovementController>(), door, new Vector3(1.28f / 2, 1.28f + 1, 0)));
        invoker.AddCommand(new DestroyCommand(go.GetComponent<CustomerDestroyer>(), customers, 1));
        invoker.isLooping = false;
    }
}
