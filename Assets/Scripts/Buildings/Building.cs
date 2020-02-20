using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building", order = 1)]
public class Building : ScriptableObject
{
    public GameObject prefab;

    [Header("Building Stats")]
    public string displayName;
    public string description;
    public float cashPerSecond;
}
