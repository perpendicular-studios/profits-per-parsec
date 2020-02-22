using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building", order = 1)]
public class Building : ScriptableObject
{
    [Header("Building Display")]
    public GameObject buildingModelPrefab;
    public GameObject buildingUIPrefab;
    public Sprite image;

    [Header("Building Stats")]
    public string displayName;
    public string description;
    public float cashPerSecond;
}
