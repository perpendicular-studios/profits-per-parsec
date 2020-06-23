using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBuildingNotificationPredicate : MonoBehaviour, INotificationPredicate
{
    public bool EvaluatePredicate()
    {
        int currentBuildings = BuildingController.instance.GetBuildingInfoListForPlanet(PlayerStatController.instance.currentPlanet).Count;
        int maxBuildings = PlayerStatController.instance.maxBuildings;

        Debug.Log($"Checking currentBuildings={currentBuildings} > maxBuildings={maxBuildings}");
        return currentBuildings > maxBuildings;
    }

}
