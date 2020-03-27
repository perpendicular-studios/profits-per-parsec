using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AdvisorIcon", order = 5)]

public class AdvisorIcons : ScriptableObject
{
    [Header("Icons")]
    public List<Sprite> icons;
}