using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Queue<Command> commandBuffer;

    private void Awake()
    {
        commandBuffer = new Queue<Command>();
    }

    public void AddCommand(Command command)
    {
        commandBuffer.Enqueue(command);
    }

    public async void Run()
    {
        while(commandBuffer.Count > 0)
        {
            Command c = commandBuffer.Dequeue();
            try
            {
                await c.Execute();
                c.Destroy();
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }

    private void OnDestroy()
    {
        for(int i = 0; i< commandBuffer.Count; ++i)
        {
            commandBuffer.Dequeue().Destroy();
        }
    }
}

