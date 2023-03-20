using Commands;
using Config;
using Controllers;
using Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Spawner
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private NameContainer nameContainer;

        public float SpawnRate = 5f;
        public int MaxCustomers = 7;
        public float MaxOrderTakeTime = 5f;
        public float MaxOrderMakeTime = 15f;

        private float timer = 0f;
        private WaitForSeconds secondsDelay;

        private readonly List<GameObject> customers = new List<GameObject>();

        private void Awake()
        {
            secondsDelay = new WaitForSeconds(120);
            nameContainer.LoadData();
        }

        private void Start()
        {
            StartCoroutine(IncrementCustomers());
        }

        private IEnumerator IncrementCustomers()
        {
            int index = 0;
            while (true)
            {
                MaxCustomers += 1 * index;
                yield return secondsDelay;
                index++;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (customers.Count < MaxCustomers && timer > SpawnRate)
            {
                SpawnCustomer();
                timer = 0f;
            }
        }

        private void SpawnCustomer()
        {
            var go = Instantiate(customerPrefab, mapGenerator.GetTile(5)[0].Position + new Vector3Int(2, 2, 0), Quaternion.identity, null);
            go.GetComponent<CustomerOrderController>().Name = nameContainer.GetRandomName();
            PrepareCommands(go);

            customers.Add(go);
        }

        private void PrepareCommands(GameObject go)
        {
            var exitDoor = mapGenerator.GetTile(15);
            var stools = mapGenerator.GetTile(10);
            var bars = mapGenerator.GetTile(8).Concat(mapGenerator.GetTile(9)).ToList();

            CommandInvoker invoker = go.GetComponent<CommandInvoker>();
            invoker.AddCommand(new MoveToCommand(new Controller[] { go.GetComponent<MovementController>() }, stools, new Vector3(1.28f / 2, 1.28f, 0)));
            invoker.AddCommand(new DelayCommand(new Controller[] { go.GetComponent<MovementController>() }, Random.Range(10, 30)));
            invoker.AddCommand(new MoveToCommand(new Controller[] { go.GetComponent<MovementController>() }, bars, new Vector3(-1.28f / 2, 2.28f, 0)));
            invoker.AddCommand(new OrderCommand(new Controller[] { go.GetComponent<CustomerOrderController>() }, MaxOrderTakeTime, MaxOrderMakeTime));
            invoker.AddCommand(new MoveToCommand(new Controller[] { go.GetComponent<MovementController>() }, exitDoor, new Vector3(1.28f / 2, 1.28f + 1, 0)));
            invoker.AddCommand(new DestroyCommand(new Controller[] { go.GetComponent<CustomerDestroyerController>() }, customers, 1));
            invoker.isLooping = false;
        }
    }
}