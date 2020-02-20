using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private GameObject activeBuilding;

    public void OnEnable()
    {
        BuildingPanel.OnBuildingClick += UpdateActiveBuilding;
    }

    public void OnDisable()
    {
        BuildingPanel.OnBuildingClick -= UpdateActiveBuilding;
    }

    public void UpdateActiveBuilding(Building building)
    {
        if (activeBuilding == null)
        {
            Vector3 mp = Input.mousePosition;
            mp.y = Terrain.activeTerrain.SampleHeight(mp);
            activeBuilding = Instantiate(building.prefab, mp, Quaternion.identity);
        }
    }

    public void Update()
    {
        if(activeBuilding != null)
        {
            activeBuilding.transform.position = Input.mousePosition;
        }
    }

}
