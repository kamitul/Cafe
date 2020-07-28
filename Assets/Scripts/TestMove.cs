using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    
    void Update()
    {
        transform.position += new Vector3(2f * Time.deltaTime * Input.GetAxis("Horizontal"), 2f * Time.deltaTime * Input.GetAxis("Vertical"), 0);
    }
}
