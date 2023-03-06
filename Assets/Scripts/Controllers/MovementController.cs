using Map;
using System;
using UnityEngine;

namespace Controllers
{
    public class MovementController : Controller, ITickable
    {
        [Range(1f, 4f)]
        public float Speed = 2f;
        private GifController gifController;

        private Vector3 destination;
        private bool isMoving = false;
        private BoxTile currentTile = null;

        private void Awake()
        {
            gifController = GetComponent<GifController>();
            StopAt(null);
        }

        public void Tick()
        {
            if (isMoving)
                transform.position = Vector3.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public float GetDistance()
        {
            return (transform.position - destination).magnitude;
        }

        public void MoveToPoint(Vector3 destination)
        {
            isMoving = true;
            gifController.Play("Walk");
            if (currentTile != null)
                currentTile.Status = TileStatus.AVAILABLE;
            this.destination = destination;
        }

        private void OnDestroy()
        {
            isMoving = false;
            if (currentTile != null)
                currentTile.Status = TileStatus.AVAILABLE;
        }

        public void StopAt(BoxTile boxTile)
        {
            isMoving = false;
            gifController.Play("Idle");
            currentTile = boxTile;
            destination = transform.position;
        }

        public void Stop()
        {
            gifController.Play("Stop");
        }
    }
}