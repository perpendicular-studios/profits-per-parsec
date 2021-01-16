using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class SectorTileInfo
{
    public Sector sector;
    public int tileNum;
    public bool hasSector;

    public SectorTileInfo(SectorTile tile)
    {
        if (tile.sector != null)
        {
            sector = tile.sector.sector;
            hasSector = true;
        }
        else
        {
            hasSector = false;
        }
    }
}

public class SectorTile : MonoBehaviour
{
    //public GridSystem grid;
    public SectorInfo sector;
}
