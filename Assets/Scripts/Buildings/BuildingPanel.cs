using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public BuildingDisplay display;

    public Building building;
    public Text displayTitle;
    public Text displayDescription;
    public Image buildingImage;

    public static event BuildingClickHandler OnBuildingClick;
    public void Update()
    {
        if (building != null)
        {
            displayTitle.text = building.displayName;
            displayDescription.text = building.description;
        }
    }

    public void OnClick()
    {
        OnBuildingClick?.Invoke(building);
        display.DisableBuildingPanels();
    }

    public delegate void BuildingClickHandler(Building building);
}
