using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    LineRenderer lr;

    // Determines the number of segments (lines) that make up the ellipse 
    [Range(3, 36)]
    public int segments;
    public Ellipse ellipse;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
    }

    //
    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];

        // Draws out each segment of the ellipse
        for (int i = 0; i < segments; i++)
        {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }
        points[segments] = points[0];

        // Sets Line Renderer 
        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void OnValidate()
    {
        if (Application.isPlaying && lr != null)
        {
            CalculateEllipse();
        }
        
    }
}
