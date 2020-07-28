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

        PrepareCommands(go);

        customers.Add(go);
    }

    private void PrepareCommands(GameObject go)
    {
        var door = mapGenerator.GetTile(5)[0];
        var stools = mapGenerator.GetTile(10);
        var bars = mapGenerator.GetTile(8).Concat(mapGenerator.GetTile(9));
        var destStool = stools.First(x => x.Status != TileStatus.OCCUPIED);
        destStool.Status = TileStatus.OCCUPIED;
        var destBar = bars.First(x => x.Status != TileStatus.OCCUPIED);
        destBar.Status = TileStatus.OCCUPIED;

        go.GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(go.GetComponent<CustomerMovementController>(), door, destStool, new Vector3(1.28f / 2, 1.28f, 0)));
        go.GetComponent<CommandInvoker>().AddCommand(new DelayCommand(go.GetComponent<CustomerMovementController>(), UnityEngine.Random.Range(3,10)));
        go.GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(go.GetComponent<CustomerMovementController>(), destStool, destBar, new Vector3(-1.28f / 2, 2.28f, 0)));
        go.GetComponent<CommandInvoker>().AddCommand(new OrderCommand());
        go.GetComponent<CommandInvoker>().AddCommand(new DelayCommand(go.GetComponent<CustomerMovementController>(), UnityEngine.Random.Range(10, 20)));
        go.GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(go.GetComponent<CustomerMovementController>(), destBar, door, new Vector3(1.28f / 2, 1.28f + 1, 0)));
        go.GetComponent<CommandInvoker>().AddCommand(new DestroyCommand(go.GetComponent<CustomerDestroyer>(), customers, 1));
    }
}
