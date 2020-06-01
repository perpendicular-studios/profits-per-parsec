using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : GameController<BuildingController>
{
    private Dictionary<Planet, List<BuildingInfo>> _allBuildings;

    public void Awake()
    {
        DateTimeController.OnDailyTick += UpdateBuildingIncome;
    }

    public List<BuildingInfo> GetBuildingInfoListForPlanet(Planet planet) {

        if (_allBuildings == null)
        {
            _allBuildings = new Dictionary<Planet, List<BuildingInfo>>();
        }

        if (!_allBuildings.ContainsKey(planet))
        {
            _allBuildings[planet] = new List<BuildingInfo>();
        }

        return _allBuildings[planet];
    }


    public void SaveBuildingForPlanet(Planet planet, Building building, float posX, float posY, float posZ)
    {
        if (_allBuildings == null)
        {
            _allBuildings = new Dictionary<Planet, List<BuildingInfo>>();
        }

        if (!_allBuildings.ContainsKey(planet))
        {
            _allBuildings[planet] = new List<BuildingInfo>();
        }

        _allBuildings[planet].Add(new BuildingInfo(building, posX, posY, posZ));
    }

    public void UpdateBuildingIncome()
    {
        foreach(List<BuildingInfo> value in _allBuildings.Values)
        {
            foreach(BuildingInfo buildingInfo in value)
            {
                PlayerStatController.instance.cash += buildingInfo.building.cashPerTick;
            }
        }
    }
}

