using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoDisplay : MonoBehaviour
{
    public Image panelBackground;
    public Text titleDisplay;
    public Image buildingImageDisplay;
    public Building selectedBuilding;

    public void Awake()
    {
        BuildingInfoController.OnBuildingClicked += OnBuildingClicked;
        DisableBuildingInfoDisplay();
    }

    public void Update()
    {
        if(selectedBuilding != null)
        {
            titleDisplay.text = selectedBuilding.displayName;
            buildingImageDisplay.sprite = selectedBuilding.image;
        }
        else
        {
            DisableBuildingInfoDisplay();
        }
    }

    public void OnBuildingClicked(Building building)
    {
        this.selectedBuilding = building;
        if (selectedBuilding != null)
        {
            EnableBuildingInfoDiplay();
        }
        else
        {
            DisableBuildingInfoDisplay();
        }
    }

    public void EnableBuildingInfoDiplay()
    {
        panelBackground.enabled = true;
        titleDisplay.enabled = true;
        buildingImageDisplay.enabled = true;
    }

    public void DisableBuildingInfoDisplay()
    {
        panelBackground.enabled = false;
        titleDisplay.enabled = false;
        buildingImageDisplay.enabled = false;
    }

}
