using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class TileInfo
{
    public Sector sector;
    public int tileNum;
    public bool hasSector;

    public TileInfo(Tile tile)
    {
        if (tile.sector != null)
        {
            sector = tile.sector.sector;
            tileNum = tile.sector.tileNum;
            hasSector = true;
        }
        else
        {
            hasSector = false;
        }
    }
}

public class Tile : MonoBehaviour
{
    //public GridSystem grid;
    public SectorInfo sector;
}
