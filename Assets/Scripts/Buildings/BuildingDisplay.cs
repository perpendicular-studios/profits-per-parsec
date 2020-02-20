using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDisplay : MonoBehaviour
{
    public Canvas canvas;
    private List<BuildingPanel> buildingPanels;

    public void Awake()
    {
        buildingPanels.AddRange(GetComponentsInChildren<BuildingPanel>());
        canvas = GetComponent<Canvas>();
    }

    public void EnableBuildingPanels()
    {
        canvas.enabled = true;
        foreach(BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = true;
            panel.displayDescription.enabled = true;
            panel.buildingImage.enabled = true;
        }
    }

    public void DisableBuildingPanels()
    {
        canvas.enabled = false;
        foreach (BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = false;
            panel.displayDescription.enabled = false;
            panel.buildingImage.enabled = false;
        }
    }
}
