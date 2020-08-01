using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To populate public fields from unity
[System.Serializable]
public class Ellipse
{
    // Initializing Variables
    public float xAxis;   //Major Axis
    public float zAxis;   //Minor Axis
    public float yAxis;   //Max Slant Height

    // Create ellipse with given size
    public Ellipse (float xAxis, float zAxis, float rotation, float yAxis)
    {
        this.xAxis = xAxis;
        this.zAxis = zAxis; 
        this.yAxis = yAxis;
    }

    // Takes a time t and returns a position vector in the ellipse
    public Vector3 Evaluate (float t)
    {
        // Solving Unit Vectors
        float magnitude = Mathf.Sqrt(Mathf.Pow(xAxis, 2) + Mathf.Pow(yAxis, 2));
        float xDirUnitVector = xAxis * 1 / magnitude;
        float yDirUnitVector = yAxis * 1 / magnitude;

        // Parametric Equation for an ellipse in 3d space
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = xAxis * Mathf.Sin(angle) * xDirUnitVector;
        float y = xAxis * Mathf.Sin(angle) * yDirUnitVector;
        float z = zAxis * Mathf.Cos(angle) * 1;
        return new Vector3(x, y, z);
    }
}
