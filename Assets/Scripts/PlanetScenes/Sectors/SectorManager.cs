using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorManager : MonoBehaviour
{

    private List<GameObject> placedSectors;
    private Sector selectedSector;

    public Material selectedSectorMaterial;
    public static Action<Tile> OnSectorSelectedAction;

    public void OnEnable()
    {
        SectorPanel.OnSectorClick += SectorHover;
        Tile.OnTileClickedAction += SelectSector;
        Tile.OnTileClickedAction += PlaceSector;
    }

    public void OnDisable()
    {
        SectorPanel.OnSectorClick -= SectorHover;
    }

    public void SectorHover(Sector sector)
    {
        Pointer.instance.setMode(PointerStatus.BUILD_OK);
        selectedSector = sector;
    }

    public void PlaceSector(Tile clickedTile)
    {
        if (selectedSector != null && !clickedTile.HasSector())
        {
            clickedTile.PlaceSector(selectedSector);    
            Pointer.instance.setMode(PointerStatus.TILE);
            SectorController.instance.SaveSectorForPlanet(clickedTile);
            SectorController.instance.selectedTile = null; //reset any previous selections of sectors on tiles
        }

        selectedSector = null; // reset selection from sector production 
    }

    public void SelectSector(Tile clickedTile)
    {
        if (selectedSector == null && clickedTile.HasSector())
        {
            Debug.Log("selecting sector..");
            clickedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial = selectedSectorMaterial;
            SectorController.instance.selectedTile = clickedTile;
            OnSectorSelectedAction?.Invoke(clickedTile);
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

}
