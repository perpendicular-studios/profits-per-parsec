using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public BuildingTerrainGrid terrainGrid;
    public Material activeBuildingMaterial;


    private GameObject activeBuilding;
    private Building activeBuildingObject;
    private List<GameObject> placedBuildings;

    // TODO : Move these to rocket UI screen
    public delegate void LaunchRocket(string startPosition, string target);
    public static event LaunchRocket OnRocketLaunch;

    public delegate void DestroyRocket(string startPosition, string target);
    public static event DestroyRocket OnRocketDestroy;
    public void Awake()
    {
        placedBuildings = new List<GameObject>();

        Planet currentPlanet = PlayerStatController.instance.currentPlanet;
        List<BuildingInfo> savedBuildingInfo = new List<BuildingInfo>();

        if (currentPlanet != null)
        {
            savedBuildingInfo = BuildingController.instance.GetBuildingInfoListForPlanet(currentPlanet);
        }

        if (savedBuildingInfo.Count > 0)
        {
            foreach(BuildingInfo buildingInfo in savedBuildingInfo)
            {
                GameObject placedBuilding = Instantiate(buildingInfo.building.buildingModelPrefab, buildingInfo.GetPositionVector(), Quaternion.identity);
                placedBuilding.layer = LayerMask.NameToLayer("Buildings");
                placedBuilding.GetComponent<MeshRenderer>().material = buildingInfo.building.buildingModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                placedBuildings.Add(placedBuilding);
            }
        }

        terrainGrid = GetComponentInChildren<BuildingTerrainGrid>();
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

    private bool IsMouseInScreen(Vector3 mousePosition)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        float mouseX = mousePosition.x;
        float mouseY = mousePosition.y;

        return (mouseX >= 0 && mouseX <= screenWidth && mouseY >= 0 && mouseY <= screenHeight);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RocketController.instance.CreateRocket("Venus", "Uranus");
        }

        if (activeBuilding != null)
        {
            activeBuilding.layer = LayerMask.NameToLayer("ActiveBuilding");
            activeBuilding.GetComponent<MeshRenderer>().material = activeBuildingMaterial;

            terrainGrid.activeBuilding = activeBuilding;
            terrainGrid.enabled = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && IsMouseInScreen(Input.mousePosition))
            {
                activeBuilding.transform.position = new Vector3(
                    (int)hit.point.x, 
                    (int)(Terrain.activeTerrain.SampleHeight(new Vector3(hit.point.x, 0, hit.point.z)) + activeBuilding.transform.localScale.y / 2),   
                    (int)hit.point.z);

                activeBuilding.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }

        if (Input.GetMouseButton(0) && terrainGrid.canPlace)
        {
            if(activeBuilding != null)
            {
                Vector3 finalPosition = activeBuilding.transform.position;
                Quaternion finalRotation = activeBuilding.transform.rotation;

                Destroy(activeBuilding);
                activeBuilding = null;

                GameObject placedBuilding = Instantiate(activeBuildingObject.buildingModelPrefab, finalPosition, finalRotation);
                placedBuilding.layer = LayerMask.NameToLayer("Buildings");
                placedBuilding.GetComponent<MeshRenderer>().material = activeBuildingObject.buildingModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                placedBuilding.GetComponent<BuildingInfo>().building = activeBuildingObject;

                Debug.Log($"Creating building for planet: {PlayerStatController.instance.currentPlanet}");

                BuildingController.instance.SaveBuildingForPlanet(
                    PlayerStatController.instance.currentPlanet,
                    placedBuilding.GetComponent<BuildingInfo>().building,
                    placedBuilding.transform.position.x, 
                    placedBuilding.transform.position.y, 
                    placedBuilding.transform.position.z);

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
