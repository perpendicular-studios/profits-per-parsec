using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBuildingNotificationPredicate : MonoBehaviour, INotificationPredicate
{
    public bool EvaluatePredicate()
    {
        return BuildingController.instance.GetBuildingInfoListForPlanet(PlayerStatController.instance.currentPlanet).Count > PlayerStatController.instance.maxBuildings;
    }

}
