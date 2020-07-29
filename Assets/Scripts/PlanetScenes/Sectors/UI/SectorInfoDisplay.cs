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
        SectorInfoController.OnSectorClicked += OnSectorClicked;
        DisableSectorInfoDisplay();
    }

    public void Update()
    {
        if(selectedSector != null)
        {
            titleDisplay.text = selectedSector.displayName;
            sectorImageDisplay.sprite = selectedSector.image;
        }
        else
        {
            DisableSectorInfoDisplay();
        }
    }

    public void OnSectorClicked(Sector sector)
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
