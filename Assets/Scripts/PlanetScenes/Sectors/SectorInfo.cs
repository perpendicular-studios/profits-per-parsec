using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorInfo : MonoBehaviour
{
    public Sector sector;
    public int tileNum;

    public SectorInfo(Sector sector, int tileNum)
    {
        this.sector = sector;
        this.tileNum = tileNum;
    }
}
