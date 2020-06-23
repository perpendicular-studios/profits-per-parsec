using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Advisor", order = 3)]
public class Advisor : ScriptableObject
{
    [Header("Advisor Display")]
    public GameObject advisorUIPrefab;
    public Sprite advisorImage;

    [Header("Advisor Stats")]
    public string displayName;
    public int age;
    public int cost;
    public int monthlyCost;
    public int knowledge;
    public int commerce;
    public int charisma;
    public int engineering;
}
