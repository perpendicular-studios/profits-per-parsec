using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AdvisorName", order = 4)]

public class AdvisorNames : ScriptableObject
{
    [Header("Names")]
    public List<string> advisorNames;
}
