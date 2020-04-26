using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : GameController<BuildingController>
{
    public Dictionary<string, List<Building>> allBuildings;

    public void Awake()
    {
        DateTimeController.OnDailyTick += UpdateBuildingIncome;
    }

    public void UpdateBuildingIncome()
    {
        foreach(KeyValuePair<string, List<Building>> kv in allBuildings)
        {
            foreach(Building building in kv.Value)
            {
                PlayerStatController.instance.cash += building.cashPerTick;
            }
        }
    }
}
