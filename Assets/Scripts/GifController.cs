using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GifController : MonoBehaviour, ITickable
{
    [SerializeField]
    private List<Sprite> sprites;

    public int TickTime = 100;

    private SpriteRenderer spriteRenderer;
    private int index = 0;
    private CancellationTokenSource cancelToken;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cancelToken = new CancellationTokenSource();
    }

    private async void Start()
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

            spriteRenderer.sprite = sprites[++index%sprites.Count];
            await Task.Delay(TickTime);
        }
    }

    public void Tick()   
    {
        
    }

    private void OnDestroy()
    {
        cancelToken.Cancel();
    }
}
