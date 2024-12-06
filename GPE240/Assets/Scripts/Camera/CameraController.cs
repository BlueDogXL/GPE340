using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraDistance;
    public float cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + cameraDistance, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed * Time.deltaTime);
        transform.LookAt(target.position, target.forward);
    }
}
