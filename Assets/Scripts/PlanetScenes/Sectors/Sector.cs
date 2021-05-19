using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sector", order = 1)]
public class Sector : ScriptableObject
{
    [Header("Sector Display")]
    public GameObject sectorModelPrefab;
    public GameObject sectorUIPrefab;
    public Sprite image;

    [Header("Sector Stats")]
    public string displayName;
    public string description;
    public int cashPerTick;
    public int mineralsPerTick;
}
