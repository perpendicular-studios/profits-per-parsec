using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorInfoDisplay : MonoBehaviour
{
    public Image panelBackground;
    public Text titleDisplay;
    public Image sectorImageDisplay;
    public Sector selectedSector;

    public void Awake()
    {
        DisableSectorInfoDisplay();
    }

    public void OnEnable()
    {
        SectorManager.OnSectorSelectedAction += OnSectorClicked;
        SectorController.OnSectorDeselect += DisableSectorInfoDisplay;
    }

    public void OnSectorClicked(Tile sectorTile)
    {
        selectedSector = sectorTile.placedSector;
        titleDisplay.text = selectedSector.displayName;
        sectorImageDisplay.sprite = selectedSector.image;
        
        if (selectedSector != null)
        {
            EnableSectorInfoDiplay();
        }
        else
        {
            DisableSectorInfoDisplay();
        }
    }

    public void EnableSectorInfoDiplay()
    {
        if(panelBackground != null)
        {
            panelBackground.enabled = true;

        }
        if(titleDisplay != null)
        {
            titleDisplay.enabled = true;
        }
        if(sectorImageDisplay != null)
        {
            sectorImageDisplay.enabled = true;
        }
    }

    public void DisableSectorInfoDisplay()
    {
        if(panelBackground != null)
        {
            panelBackground.enabled = false;

        }
        if(titleDisplay != null)
        {
            titleDisplay.enabled = false;
        }
        if(sectorImageDisplay != null)
        {
            sectorImageDisplay.enabled = false;
        }

    }

}
