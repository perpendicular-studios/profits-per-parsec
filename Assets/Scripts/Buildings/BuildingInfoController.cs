using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoController : MonoBehaviour
{
    public Building selectedBuilding;
    public Building previousBuilding;
    public GameObject previousSelectedBuildingObject;
    public GameObject selectedBuildingObject;
    public Material selectedBuildingMaterial;

    public static event BuildingClickHandler OnBuildingClicked;
    public delegate void BuildingClickHandler(Building building);

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If object is hit by mouse
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (LayerMask.LayerToName(hitObject.layer) == "Buildings")
            {
                BuildingInfo buildingInfo = hitObject.GetComponent<BuildingInfo>();
                if (selectedBuilding != null)
                {
                    previousSelectedBuildingObject = selectedBuildingObject;
                    previousBuilding = selectedBuilding;

                    previousSelectedBuildingObject.GetComponent<MeshRenderer>().material = previousBuilding.buildingModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                }

                selectedBuildingObject = hitObject;
                selectedBuilding = buildingInfo.building;
                selectedBuildingObject.GetComponent<MeshRenderer>().material = selectedBuildingMaterial;
                OnBuildingClicked?.Invoke(selectedBuilding);
            }
            else
            {
                if(selectedBuilding != null)
                {
                    selectedBuildingObject.GetComponent<MeshRenderer>().material = selectedBuilding.buildingModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                }
                selectedBuildingObject = null;
                selectedBuilding = null;
                OnBuildingClicked?.Invoke(selectedBuilding);
            }
        }
        
    }
}
