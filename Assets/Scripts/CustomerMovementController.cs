using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CustomerMovementController : MonoBehaviour, ITickable
{
    [Range(1f,4f)]
    public float Speed = 2f;
    private GifController gifController;

    private Vector3 destination;
    private bool isMoving = false;

    private void Awake()
    {
        gifController = GetComponent<GifController>();
        Stop();
    }

    public void Tick()
    {
        if(isMoving)
            transform.position = Vector3.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
    }

    public float GetDistance()
    {
        return (transform.position - destination).magnitude;
    }

    public void MoveToPoint(Vector3 destination)
    {
        isMoving = true;
        gifController.Play("Walk");
        this.destination = destination;
    }

    public void Stop()
    {
        isMoving = false;
        gifController.Play("Idle");
        this.destination = transform.position;
    }

    public void Sit()
    {
    }
}
