using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private Queue<Command> commandBuffer;
    public bool isLooping = false;

    private void Awake()
    {
        commandBuffer = new Queue<Command>();
    }

    private void Start()
    {
        Run();
    }

    public void AddCommand(Command command)
    {
        commandBuffer.Enqueue(command);
    }

    public async void Run()
    {
        if (!isLooping)
        {
            while (commandBuffer.Count > 0)
            {
                Command c = commandBuffer.Dequeue();
                try
                {
                    await c.Execute();
                    c.Destroy();
                }
                catch (TaskCanceledException)
                {
                }
            }
        }
        else
        {
            while(true)
            {
                for(int i = 0; i < commandBuffer.Count; ++i)
                {
                    Command c = commandBuffer.ElementAt(i);
                    try
                    {
                        await c.Execute();
                    }
                    catch (TaskCanceledException)
                    {
                    }
                }
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

