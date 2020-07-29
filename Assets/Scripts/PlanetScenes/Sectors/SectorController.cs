using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorController : GameController<SectorController>
{
    private Dictionary<Planet, List<SectorInfo>> _allSectors;

    public void Awake()
    {
        DateTimeController.OnDailyTick += UpdateSectorIncome;
    }

    public List<SectorInfo> GetSectorInfoListForPlanet(Planet planet) {

        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<SectorInfo>>();
        }

        if (!_allSectors.ContainsKey(planet))
        {
            _allSectors[planet] = new List<SectorInfo>();
        }

        return _allSectors[planet];
    }


    public void SaveSectorForPlanet(Planet planet, Sector sector, int tileNum)
    {
        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<SectorInfo>>();
        }

        if (!_allSectors.ContainsKey(planet))
        {
            _allSectors[planet] = new List<SectorInfo>();
        }

        _allSectors[planet].Add(new SectorInfo(sector, tileNum));

    }

    public void UpdateSectorIncome()
    {
        foreach(List<SectorInfo> value in _allSectors.Values)
        {
            foreach(SectorInfo sectorInfo in value)
            {
                PlayerStatController.instance.cash += sectorInfo.sector.cashPerTick;
            }
        }
    }
}

