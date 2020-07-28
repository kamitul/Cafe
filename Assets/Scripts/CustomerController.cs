using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField]
    private List<ITickable> tickables;

    private GifController gifController;

    private void Awake()
    {
        tickables = GetComponents<ITickable>().ToList();
        gifController = GetComponent<GifController>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(movement != Vector2.zero)
        {
            gifController.Play("Walk");
            transform.position += new Vector3(2f * Time.deltaTime * Input.GetAxis("Horizontal"), 2f * Time.deltaTime * Input.GetAxis("Vertical"), 0);
        }
        else
        {
            gifController.Play("Idle");
        }

        for(int i = 0; i < tickables.Count; ++i)
        {
            tickables[i].Tick();
        }
    }
}
