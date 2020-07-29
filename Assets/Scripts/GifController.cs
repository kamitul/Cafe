using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GifController : MonoBehaviour, ITickable
{
    public List<SpriteAnimation> Animations;
    public int TickTime = 100;

    private SpriteRenderer spriteRenderer;
    private int index = 0;
    private CancellationTokenSource cancelToken;
    private SpriteAnimation currentAnimation;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cancelToken = new CancellationTokenSource();
        Initialize();
    }

    public void Play(string name)
    {
        if (!currentAnimation || currentAnimation.name != name)
        {
            cancelToken = new CancellationTokenSource();
            currentAnimation = Animations.Find(x => x.Name == name);
        }
    }

    public void Stop()
    {
        cancelToken.Cancel();
        cancelToken.Dispose();      
    }

    public void Tick()   
    {
        
    }

    private async void Initialize()
    {
        try
        {
            await AnimateGif();
        }
        catch (TaskCanceledException)
        {
            return;
        }
    }

    private async Task AnimateGif()
    {
        while (true)
        {
            if (cancelToken.IsCancellationRequested)
                throw new TaskCanceledException();

            if(currentAnimation)
                spriteRenderer.sprite = currentAnimation.Sprites[++index % currentAnimation.Sprites.Count];

            await Task.Delay(TickTime);
        }
    }

    private void OnDestroy()
    {
        Stop();
    }
}
