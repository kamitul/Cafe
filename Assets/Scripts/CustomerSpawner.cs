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

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if(customers.Count < 5 && timer > SpawnRate)
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
        var stools = mapGenerator.GetTile(10);
        var bars = mapGenerator.GetTile(8).Concat(mapGenerator.GetTile(9));
        var destStool = stools.First(x => x.Status != TileStatus.OCCUPIED);
        destStool.Status = TileStatus.OCCUPIED;
        var destBar = bars.First(x => x.Status != TileStatus.OCCUPIED);
        destBar.Status = TileStatus.OCCUPIED;

        go.GetComponent<CommandInvoker>().AddCommand(new MoveToCommand(go.GetComponent<CustomerMovementController>(), mapGenerator.GetTile(5)[0], destStool));
        go.GetComponent<CommandInvoker>().AddCommand(new DelayCommand(go.GetComponent<CustomerMovementController>(), 4));
        go.GetComponent<CommandInvoker>().AddCommand(new OrderCommand(go.GetComponent<CustomerMovementController>(), destStool, destBar));
        go.GetComponent<CommandInvoker>().AddCommand(new DelayCommand(go.GetComponent<CustomerMovementController>(), 4));
    }
}
