using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private GameObject activeBuilding;
    private Building activeBuildingObject;
    private List<GameObject> placedBuildings;

    public void Awake()
    {
        placedBuildings = new List<GameObject>();
    }

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
            activeBuilding = Instantiate(building.buildingModelPrefab);
            activeBuildingObject = building;
        }
    }

    public void Update()
    {
        if(activeBuilding != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                float hitPointX = hit.point.x;
                float hitPointZ = hit.point.z;
                float terrainHeight = Terrain.activeTerrain.SampleHeight(new Vector3(hitPointX, 0, hitPointZ));

                activeBuilding.transform.position = new Vector3(hitPointX, terrainHeight, hitPointZ);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if(activeBuilding != null)
            {
                Vector3 finalPosition = activeBuilding.transform.position;

                Destroy(activeBuilding);
                activeBuilding = null;

                GameObject placedBuilding = Instantiate(activeBuildingObject.buildingModelPrefab, finalPosition, Quaternion.identity);
                placedBuildings.Add(placedBuilding);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Delete all buildings
            foreach(GameObject placedBuilding in placedBuildings)
            {
                Destroy(placedBuilding);
            }
            placedBuildings.Clear();
        }
    }

}
