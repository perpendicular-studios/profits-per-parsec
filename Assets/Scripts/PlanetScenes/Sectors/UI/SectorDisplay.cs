using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorDisplay : MonoBehaviour
{
    public List<Sector> sectors;
    public Transform panelParent;
    public Image shopBackground;
    
    private List<SectorPanel> sectorPanels;
    private bool isEnabled;

    public void Awake()
    {
        shopBackground = GetComponent<Image>();

        GenerateSectorPanels();
        DisableSectorPanels();
    }

    private void GenerateSectorPanels()
    {
        foreach(Sector sector in sectors)
        {
            SectorPanel panel = Instantiate(sector.sectorUIPrefab, panelParent).GetComponent<SectorPanel>();
            panel.sector = sector;
        }

        sectorPanels = new List<SectorPanel>();
        sectorPanels.AddRange(GetComponentsInChildren<SectorPanel>());
    }

    public void Update()
    {

    }

    public void EnableSectorPanels()
    {
        shopBackground.enabled = true;
        foreach(SectorPanel panel in sectorPanels)
        {
            panel.displayTitle.enabled = true;
            panel.displayDescription.enabled = true;
            panel.sectorImage.enabled = true;
            panel.panelBackground.enabled = true;
        }
        isEnabled = true;
    }

    public void DisableSectorPanels()
    {
        shopBackground.enabled = false;
        foreach (SectorPanel panel in sectorPanels)
        {
            panel.displayTitle.enabled = false;
            panel.displayDescription.enabled = false;
            panel.sectorImage.enabled = false;
            panel.panelBackground.enabled = false;
        }
        isEnabled = false;
    }
}
