using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;

    [SerializeField]
    private GameObject playerPrefab;


    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var go = Instantiate(playerPrefab, (Vector3)mapGenerator.GetTile(13)[UnityEngine.Random.Range(0, mapGenerator.GetTile(13).Count)].Position * 1.28f, Quaternion.identity, null);
        PrepareCommands(go);
    }

    private void PrepareCommands(GameObject go)
    {
        var tiles = mapGenerator.GetTile(13);

        CommandInvoker invoker = go.GetComponent<CommandInvoker>();
        invoker.AddCommand(new MoveToCommand(go.GetComponent<MovementController>(), tiles, new Vector3(1.28f / 2, 1.28f, 0)));
        invoker.AddCommand(new DelayCommand(go.GetComponent<MovementController>(), UnityEngine.Random.Range(3, 10)));
        invoker.AddCommand(new MoveToCommand(go.GetComponent<MovementController>(), tiles, new Vector3(1.28f / 2, 1.28f, 0)));
        invoker.AddCommand(new DelayCommand(go.GetComponent<MovementController>(), UnityEngine.Random.Range(3, 10)));
        invoker.isLooping = true;
    }
}
