using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    public GridSystem grid;
    public Material activeSectorMaterial;

    private List<GameObject> placedSectors;

    // TODO : Move these to rocket UI screen
    public delegate void LaunchRocket(string startPosition, string target);
    public static event LaunchRocket OnRocketLaunch;

    public delegate void DestroyRocket(string startPosition, string target);
    public static event DestroyRocket OnRocketDestroy;
    public void Start()
    {
        placedSectors = new List<GameObject>();

        Planet currentPlanet = PlayerStatController.instance.currentPlanet;
        List<SectorInfo> savedSectorInfo = new List<SectorInfo>();

        if (currentPlanet != null)
        {
            savedSectorInfo = SectorController.instance.GetSectorInfoListForPlanet(currentPlanet);
        }

        if (savedSectorInfo.Count > 0)
        {
            foreach(SectorInfo sectorInfo in savedSectorInfo)
            {
                GameObject placedSector = Instantiate(sectorInfo.sector.sectorModelPrefab, grid.tileList[sectorInfo.GetTileNum()].gameObject.transform.position, Quaternion.identity);
                //Reposition sector
                placedSector.transform.position += new Vector3(grid.tileSize / 2, 0, grid.tileSize / 2);
                //Set parent
                placedSector.transform.parent = grid.tileList[sectorInfo.GetTileNum()].gameObject.transform;
                placedSector.layer = LayerMask.NameToLayer("Sectors");
                placedSector.GetComponent<MeshRenderer>().material = sectorInfo.sector.sectorModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                placedSectors.Add(placedSector);
            }
        }
    }

    public void OnEnable()
    {
        SectorPanel.OnSectorClick += PlaceSector;
    }

    public void OnDisable()
    {
        SectorPanel.OnSectorClick -= PlaceSector;
    }

    public void PlaceSector(Sector sector)
    {
        GameObject placedSector = Instantiate(sector.sectorModelPrefab, grid.tileList[grid.currTile].gameObject.transform);
        placedSector.transform.position += new Vector3(grid.tileSize / 2, 0, grid.tileSize / 2);
        placedSector.GetComponent<SectorInfo>().sector = sector;
        SectorController.instance.SaveSectorForPlanet(
                    PlayerStatController.instance.currentPlanet,
                    placedSector.GetComponent<SectorInfo>().sector,
                    grid.currTile);

        //Let the tile know a sector has been placed on it
        grid.tileList[grid.currTile].hasSector = true;
        TileController.instance.SaveTileForPlanet(PlayerStatController.instance.currentPlanet, grid.tileList);

        // Check notifications when sector is placed.
        NotificationController.instance.UpdateNotifications();

        placedSectors.Add(placedSector);
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
            // Delete all sectors
            foreach(GameObject placedSector in placedSectors)
            {
                Destroy(placedSector);
            }
            placedSectors.Clear();
        }
    }

}
