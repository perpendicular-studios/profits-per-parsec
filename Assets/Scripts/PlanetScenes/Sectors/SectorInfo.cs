using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorInfo : MonoBehaviour
{
    public Sector sector;
    public Material defaultSectorMaterial;
    public bool isRocketBase;
    public string planetDestinationName;

    public static Action<Tile> OnSectorModelSelected; 

    public void OnMouseDown()
    {
        OnSectorModelSelected?.Invoke(GetComponentInParent<Tile>());
    }
}
