﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CustomerMovementController : MonoBehaviour, ITickable
{
    [Range(0f,1f)]
    public float Speed = 0.25f;
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
            transform.position = Vector3.Lerp(transform.position, destination, Speed * Time.deltaTime);
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
