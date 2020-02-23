using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDisplay : MonoBehaviour
{
    public List<Building> buildings;
    public Transform panelParent;
    public Image shopBackground;
    
    private List<BuildingPanel> buildingPanels;
    private bool isEnabled;

    public void Awake()
    {
        shopBackground = GetComponent<Image>();

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
        shopBackground.enabled = true;
        foreach(BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = true;
            panel.displayDescription.enabled = true;
            panel.buildingImage.enabled = true;
            panel.panelBackground.enabled = true;
        }
        isEnabled = true;
    }

    public void DisableBuildingPanels()
    {
        shopBackground.enabled = false;
        foreach (BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = false;
            panel.displayDescription.enabled = false;
            panel.buildingImage.enabled = false;
            panel.panelBackground.enabled = false;
        }
        isEnabled = false;
    }
}
