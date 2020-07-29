using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Command
{
    protected CancellationTokenSource token = new CancellationTokenSource();

    public virtual async Task Execute() { }
    public void Destroy()
    {
        token.Cancel();
    }
}

