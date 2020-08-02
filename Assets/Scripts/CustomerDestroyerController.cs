using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyerController : Controller
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
