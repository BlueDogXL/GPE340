using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color color;
    public float lifespan = 0.1f;
    public float width = 0.5f;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // get our line renderer
        lineRenderer = GetComponent<LineRenderer>();
        // set our color
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        // set our width
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        // get our points
        Vector3[] points = { startPoint, endPoint };
        // set our points (aka fire)
        lineRenderer.SetPositions(points);
        // leave after the shot's over
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
