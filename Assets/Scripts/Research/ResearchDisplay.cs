using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchDisplay : MonoBehaviour
{
    public Image researchBackground;
    private List<BuildingPanel> buildingPanels;
    private bool isEnabled;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isEnabled)
            {
                DisableResearchPanels();
            }
            else
            {
                EnableResearchPanels();
            }
        }
    }

    public void EnableResearchPanels()
    {
        researchBackground.enabled = true;
        foreach (BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = true;
            panel.displayDescription.enabled = true;
            panel.buildingImage.enabled = true;
            panel.panelBackground.enabled = true;
        }
        isEnabled = true;
    }

    public void DisableResearchPanels()
    {
        researchBackground.enabled = false;
        foreach (BuildingPanel panel in buildingPanels)
        {
            panel.displayTitle.enabled = false;
            panel.displayDescription.enabled = false;
            panel.buildingImage.enabled = false;
            panel.panelBackground.enabled = false;
        }
        isEnabled = false;
    }
}
