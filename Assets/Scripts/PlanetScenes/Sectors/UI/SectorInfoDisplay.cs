﻿using System;
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
    public Tile selectedTile;
    public GameObject destinationButton;
    public Text planetDestinationTitle;
    public Text planetDestinationName;

    public static Action<Tile> SelectRocketDestinationEvent;
    
    public void Awake()
    {
        DisableSectorInfoDisplay();
    }

    public void OnEnable()
    {
        SectorManager.OnSectorSelectedAction += OnSectorClicked;
        
        SectorManager.OnRocketDestinationSelection += DisableSectorInfoDisplay;
        SectorController.OnSectorDeselectNothing += DisableSectorInfoDisplay;
    }

    public void OnSectorClicked(Tile sectorTile)
    {
        selectedSector = sectorTile.placedSector;
        selectedTile = sectorTile;
        
        if (selectedSector != null)
        {
            titleDisplay.text = selectedSector.displayName;
            sectorImageDisplay.sprite = selectedSector.image;
            planetDestinationName.text = sectorTile.placedSectorObject.GetComponent<SectorInfo>().planetDestinationName;
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

        if (selectedSector.sectorModelPrefab.GetComponent<SectorInfo>().isRocketBase)
        {
            destinationButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                SelectRocketDestinationEvent?.Invoke(selectedTile);
            });
            
            if (planetDestinationTitle != null)
            {
                planetDestinationTitle.enabled = true;
            }

            if (planetDestinationName != null)
            {
                planetDestinationName.enabled = true;
            }

            if (destinationButton != null)
            {
                destinationButton.SetActive(true);
            }
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

        if (destinationButton != null)
        {
            destinationButton.SetActive(false);
        }

        if (planetDestinationTitle != null)
        {
            planetDestinationTitle.enabled = false;
        }

        if (planetDestinationName != null)
        {
            planetDestinationName.enabled = false;
        }
    }

}
