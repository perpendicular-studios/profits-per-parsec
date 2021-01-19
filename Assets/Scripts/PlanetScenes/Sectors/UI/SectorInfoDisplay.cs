using System;
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
    public GameObject destinationButton;

    public static Action<Transform> SelectRocketDestinationEvent;
    
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
        destinationButton.GetComponent<Button>().onClick.AddListener(delegate { SelectRocketDestinationEvent?.Invoke(sectorTile.transform); });
        
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

        if (destinationButton != null)
        {
            if (selectedSector.sectorModelPrefab.layer == LayerMask.NameToLayer("RocketBase"))
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

    }

}
