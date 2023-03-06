using Controllers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Command
{
    protected CancellationTokenSource token = new CancellationTokenSource();
    protected List<Controller> controllers;

    public virtual async Task Execute() { }

    public Command(dynamic[] controllers) 
    {
        this.controllers = new List<Controller>();
        for(int i = 0; i < controllers.Length; ++i)
        {
            this.controllers.Add(controllers[i] as Controller);
        }
    }

    public void Destroy()
    {
        token.Cancel();
    }
}

