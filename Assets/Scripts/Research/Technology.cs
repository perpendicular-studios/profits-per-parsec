using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResearchCategory { Rocketry, Tourism, Government, Engineering, Espionage, Employee};

[CreateAssetMenu(menuName = "Scriptable Objects/Technology", order = 2)]
public class Technology : ScriptableObject
{
    [Header("Tehnology Display")]
    public GameObject researchUIPrefab;
    public Sprite lockedImage;
    public Sprite unlockedImage;
    public Sprite currentImage;

    [Header("Technology Stats")]
    public string displayName;
    public string description;
    public int researchCost;
    public int tier;
    public ResearchCategory researchCategory;
    public bool inProgress;
    public bool isLocked;
    public List<Technology> prerequisite; 
}
