using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public Building building;
    public Text displayTitle;
    public Text displayDescription;
    public Image buildingImage;

    public void Update()
    {
        if(building != null)
        {
            displayTitle.text = building.displayName;
            displayDescription.text = building.description;
            
        }
    }

}
