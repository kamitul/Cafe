using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Queue<ICommand> commandBuffer;

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
    }

    public void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }

    public async void Run()
    {
        while(commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            await c.Execute();
        }
    }
}

