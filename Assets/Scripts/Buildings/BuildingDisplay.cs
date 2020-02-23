using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDisplay : MonoBehaviour
{
    public List<Building> buildings;
    public Transform panelParent;

    private Canvas canvas;
    private List<BuildingPanel> buildingPanels;
    private bool isEnabled;

    public void Awake()
    {
        canvas = GetComponent<Canvas>();

        GenerateBuildingPanels();
        DisableBuildingPanels();
    }

    private void GenerateBuildingPanels()
    {
        foreach(Building building in buildings)
        {
            BuildingPanel panel = Instantiate(building.buildingUIPrefab, panelParent).GetComponent<BuildingPanel>();
            panel.building = building;
        }

        buildingPanels = new List<BuildingPanel>();
        buildingPanels.AddRange(GetComponentsInChildren<BuildingPanel>());
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (isEnabled)
            {
                DisableBuildingPanels();
            }
            else
            {
                EnableBuildingPanels();
            }
        }
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
        isEnabled = true;
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
        isEnabled = false;
    }
}
