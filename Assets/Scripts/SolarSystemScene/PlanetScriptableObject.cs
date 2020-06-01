using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlanetScriptableObject", order = 4)]
public class PlanetScriptableObject : ScriptableObject
{
    [Header("Planet Data")]
    public string planetName;

    [Header("Orbit Data")]
    public int orbitPathX;
    public int orbitPathY;
    public int orbitPathZ;
    [Range(0f, 1f)]
    public float orbitProgress;
    public int orbitPeriod;
    public bool orbitActive;
    public int segments;

    [Header("Rotation Data")]
    public int rotationSpeed;
    public int dampAmt;
    public GameObject model;
    public PlanetScriptableObject orbiting;
    public bool hasMoon;
    public bool isMoon;
}
