using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorInfo : MonoBehaviour
{
    private Sector _sector;
    public Sector sector
    {
        get { return _sector; }
        set { _sector = value; }
    }

    private int _tileNum;

    public SectorInfo(Sector sector, int tileNum)
    {
        _sector = sector;
        _tileNum = tileNum;
    }

    public int GetTileNum()
    {
        return _tileNum;
    }
}
