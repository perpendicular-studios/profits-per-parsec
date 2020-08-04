using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorInfoDisplay : MonoBehaviour
{
    public Image panelBackground;
    public Text titleDisplay;
    public Image sectorImageDisplay;
    public SectorInfo selectedSector;

    public void Awake()
    {
        DisableSectorInfoDisplay();
    }

    public void OnEnable()
    {
        GridSystem.OnSectorClicked += OnSectorClicked;
        GridSystem.OnSectorDeselect += DisableSectorInfoDisplay;
    }

    public void Update()
    {
        if(selectedSector != null)
        {
            titleDisplay.text = selectedSector.sector.displayName;
            sectorImageDisplay.sprite = selectedSector.sector.image;
        }
        else
        {
            DisableSectorInfoDisplay();
        }
    }

    public void OnSectorClicked(SectorInfo sector)
    {
        this.selectedSector = sector;
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
        panelBackground.enabled = true;
        titleDisplay.enabled = true;
        sectorImageDisplay.enabled = true;
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
