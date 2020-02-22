using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingPanel : MonoBehaviour
{
    
    public Building building;
    public Text displayTitle;
    public Text displayDescription;
    public Image buildingImage;

    private Button button;
    private BuildingDisplay display;

    public static event BuildingClickHandler OnBuildingClick;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClick(); });
        display = GetComponentInParent<BuildingDisplay>();
    }

    public void Update()
    {
        if (building != null)
        {
            displayTitle.text = building.displayName;
            displayDescription.text = building.description;
            buildingImage.sprite = building.image;
        }
    }

    public void OnClick()
    {
        OnBuildingClick?.Invoke(building);
        display.DisableBuildingPanels();
    }

    public delegate void BuildingClickHandler(Building building);
}
