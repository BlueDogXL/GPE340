using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraDistance;
    public float cameraSpeed;

    private void Start()
    {
        if (target == null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                if (player.pawn != null)
                {
                    target = player.pawn.transform;
                }
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        // if the game is paused don't do anything
        if (GameManager.instance.isPaused) return;
        if (target != null)
        {
            // calculate where we should be
            Vector3 newPosition = new Vector3(target.position.x, target.position.y + cameraDistance, target.position.z);
            // move towards that position
            transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed * Time.deltaTime);
            // look at our target (because that's kind of the camera's job)
            transform.LookAt(target.position, target.forward);
        }
    }
}
