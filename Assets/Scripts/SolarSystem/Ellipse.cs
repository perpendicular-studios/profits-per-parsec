using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To populate public fields from unity
[System.Serializable]
public class Ellipse
{
    // Initializing Variables
    public float xAxis;
    public float yAxis;

    // Create ellipse with given size
    public Ellipse (float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    // Takes a time t and returns a position vector in the ellipse
    public Vector2 Evaluate (float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
}
