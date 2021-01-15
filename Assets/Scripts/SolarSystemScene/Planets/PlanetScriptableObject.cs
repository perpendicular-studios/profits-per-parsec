using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PlanetScriptableObject", order = 4)]
public class PlanetScriptableObject : ScriptableObject
{
    [Header("Planet Data")]
    public string planetName;
    public int order;
    public float worldScale;

    [Header("Orbit Data")]
    public int orbitPathX;
    public int orbitPathY;
    public int orbitPathZ;
    [Range(0f, 1f)]
    public float orbitProgress;
    public int orbitPeriod;
    public bool orbitActive;
    public int segments;

    [Header("Texture Data")] 
    public Material lockedMaterial;
    public Material planetHexMaterial;
    public Material planetPentMaterial;
    

    [Header("Rotation Data")]
    public int rotationSpeed;
    public int dampAmt;
    public PlanetScriptableObject orbiting;
    public bool hasMoon;
    public bool isMoon;
    public bool innerPlanet;

    [Header("Position Data")] 
    public int zOffset;
}
