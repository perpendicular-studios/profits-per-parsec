using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{

    private List<GameObject> placedSectors;
    private Sector selectedBuildSector;

    public static Action<Tile> OnSectorSelectedAction;
    public static Action OnRocketDestinationSelection;
    
    public Material selectedSectorMaterial;
    public Material validSelectionSectorMaterial;

    public GameObject startRocketBaseTile;
    
    public void OnEnable()
    {
        SectorPanel.OnSectorClick += SectorHover;
        
        Tile.OnTileClickedAction += OnTileClicked;
        SectorInfo.OnSectorModelSelected += OnTileClicked;
        
        Tile.OnTileClickedAction += PlaceSector;
        SectorInfoDisplay.SelectRocketDestinationEvent += SelectAllRocketBases;
    }

    public void SectorHover(Sector sector)
    {
        Pointer.instance.setMode(PointerStatus.BUILD_OK);
        selectedBuildSector = sector;
        SectorController.instance.isBuilding = true;
    }

    public void OnTileClicked(Tile clickedTile)
    {
        if (SectorController.instance.isSelectingRocketDestination)
        {
            // select rocket destination
            if (clickedTile.placedSector.sectorModelPrefab.GetComponent<SectorInfo>().isRocketBase)
            {
                startRocketBaseTile.GetComponentInChildren<SectorInfo>().planetDestinationName =
                    clickedTile.parentPlanet.planet.planetName;
                
                RocketController.instance.CreateConnection(startRocketBaseTile.transform, clickedTile.transform);
                
                // Deselect previous tile
                startRocketBaseTile.GetComponent<Tile>().DeselectTile();
                if (SectorController.instance.selectedTile != null)
                {
                    SectorController.instance.selectedTile.DeselectTile();
                    SectorController.instance.selectedTile = null;
                }
                
                // Deselect all other rocket bases from being highlighted, update UI
                DeselectAllRocketBases();
            }
        }
        else {

            // before selecting the tile, deselect previously selected tile
            if (SectorController.instance.selectedTile != null)
            {
                SectorController.instance.selectedTile.DeselectTile();
                SectorController.instance.selectedTile = null;
            }

            if (selectedBuildSector != null) //if queued for build
            {
                if (!clickedTile.HasSector())
                {
                    PlaceSector(clickedTile);
                }
            }
            else
            {
                clickedTile.SelectTile();
                SectorController.instance.selectedTile = clickedTile;

                if (clickedTile.HasSector())
                {
                    SelectSector(clickedTile);
                }
            }
        }
    }

    public void PlaceSector(Tile clickedTile)
    {
        if (selectedBuildSector != null && !clickedTile.HasSector())
        {
            clickedTile.PlaceSector(selectedBuildSector);    
            Pointer.instance.setMode(PointerStatus.TILE);
            SectorController.instance.SaveSectorForPlanet(clickedTile);
            SectorController.instance.selectedTile = null; //reset any previous selections of sectors on tiles
        }

        selectedBuildSector = null; // reset selection from sector production 
        SectorController.instance.isBuilding = false;
    }

    public void SelectSector(Tile clickedTile)
    {
        if (selectedBuildSector == null && clickedTile.HasSector()) //if not building anything
        {
            clickedTile.placedSectorObject.GetComponent<MeshRenderer>().sharedMaterial =
                selectedSectorMaterial;

            OnSectorSelectedAction?.Invoke(clickedTile);
        }
        

    }

    public void SelectAllRocketBases(Tile startTile)
    {
        SectorController.instance.isSelectingRocketDestination = true;
        startRocketBaseTile = startTile.gameObject;
        
        foreach(GameObject obj in SectorController.instance.rocketBuildings)
        {
            obj.GetComponentInChildren<MeshRenderer>().material = validSelectionSectorMaterial;
        }
    }

    public void DeselectAllRocketBases()
    {
        SectorController.instance.isSelectingRocketDestination = false;
        foreach(GameObject obj in SectorController.instance.rocketBuildings)
        {
            obj.GetComponentInChildren<MeshRenderer>().material = obj.GetComponent<SectorInfo>().defaultSectorMaterial;
        }
        OnRocketDestinationSelection?.Invoke();
    }

    private bool IsMouseInScreen(Vector3 mousePosition)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        float mouseX = mousePosition.x;
        float mouseY = mousePosition.y;

        return (mouseX >= 0 && mouseX <= screenWidth && mouseY >= 0 && mouseY <= screenHeight);
    }

}
