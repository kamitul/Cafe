using Controllers;
using Map;
using UnityEngine;

namespace Spawner
{
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
            var tile = mapGenerator.GetTile(13)[Random.Range(0, mapGenerator.GetTile(13).Count)];
            var go = Instantiate(playerPrefab, (Vector3)tile.Position * 1.28f, Quaternion.identity, null);
            go.GetComponent<MovementController>().StopAt(tile);
            PrepareCommands(go);
        }

        private void PrepareCommands(GameObject go)
        {
            var tiles = mapGenerator.GetTile(13);

            CommandInvoker invoker = go.GetComponent<CommandInvoker>();
            invoker.AddCommand(new MoveToCommand(new Controller[] { go.GetComponent<MovementController>() }, tiles, new Vector3(1.28f / 2, 1.28f, 0)));
            invoker.AddCommand(new DelayCommand(new Controller[] { go.GetComponent<MovementController>() }, Random.Range(3, 10)));
            invoker.AddCommand(new MoveToCommand(new Controller[] { go.GetComponent<MovementController>() }, tiles, new Vector3(1.28f / 2, 1.28f, 0)));
            invoker.AddCommand(new DelayCommand(new Controller[] { go.GetComponent<MovementController>() }, Random.Range(3, 10)));
            invoker.isLooping = true;
        }
    }
}