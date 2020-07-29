using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSectorNotificationPredicate : MonoBehaviour, INotificationPredicate
{
    public bool EvaluatePredicate()
    {
        int currentSectors = SectorController.instance.GetSectorInfoListForPlanet(PlayerStatController.instance.currentPlanet).Count;
        int maxSectors = PlayerStatController.instance.maxSectors;

        Debug.Log($"Checking currentSectors={currentSectors} > maxSectors={maxSectors}");
        return currentSectors > maxSectors;
    }

}
