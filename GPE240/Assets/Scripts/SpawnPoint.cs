using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float gizmoBoxHeight = 2;
    private float gizmoBoxWidth = 1;

    public void OnDrawGizmos()
    {
        Color boxColor = Color.yellow;
        boxColor.a = 0.7f;
        Gizmos.color = boxColor;

        Vector3 boxPosition = transform.position;
        boxPosition += Vector3.up * (gizmoBoxHeight / 2);
        Vector3 boxSize = new Vector3(gizmoBoxWidth, gizmoBoxHeight, gizmoBoxWidth);
        Gizmos.DrawCube(boxPosition, boxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(boxPosition, transform.forward);
    }
}
