using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Technology", order = 2)]
public class Technology : ScriptableObject
{
    [Header("Tehnology Display")]
    public GameObject researchUIPrefab;
    public Sprite lockedImage;
    public Sprite unlockedImage;

    [Header("Technology Stats")]
    public string displayName;
    public string description;
    public int researchCost;
    public bool isLocked;
    public List<Technology> prerequisite; 
}
