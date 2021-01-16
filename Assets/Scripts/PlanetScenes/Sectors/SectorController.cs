using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorController : GameController<SectorController>
{
    private Dictionary<Planet, List<Sector>> _allSectors;

    public void Awake()
    {
        DateTimeController.OnDailyTick += UpdateSectorIncome;
    }

    public List<Sector> GetSectorInfoListForPlanet(Planet planet) {

        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<Sector>>();
        }

        if (!_allSectors.ContainsKey(planet))
        {
            _allSectors[planet] = new List<Sector>();
        }

        return _allSectors[planet];
    }


    public void SaveSectorForPlanet(Tile sectorTile)
    {
        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<Sector>>();
        }

        if (!_allSectors.ContainsKey(sectorTile.parentPlanet.planet))
        {
            _allSectors[sectorTile.parentPlanet.planet] = new List<Sector>();
        }

        _allSectors[sectorTile.parentPlanet.planet].Add(sectorTile.placedSector);

    }

    public void UpdateSectorIncome()
    {
        foreach(List<Sector> sectorInfoList in _allSectors.Values)
        {
            foreach(Sector sectorInfo in sectorInfoList)
            {
                PlayerStatController.instance.cash += sectorInfo.cashPerTick;
            }
        }
    }
}

