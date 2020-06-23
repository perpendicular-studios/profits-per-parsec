
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To populate public fields from unity
[System.Serializable]
public class Planet
{

    public string planetName;

    public int orbitPathX;
    public int orbitPathY;
    public int orbitPathZ;
    public float orbitProgress;
    public int orbitPeriod;
    public bool orbitActive;
    public int segments;

    public bool hasMoon;
    public bool isMoon;
    public int rotationSpeed;
    public int dampAmt;
    public GameObject model;
    public Planet orbiting;
}
