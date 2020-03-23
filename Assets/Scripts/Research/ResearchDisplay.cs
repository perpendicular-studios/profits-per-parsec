using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchDisplay : MonoBehaviour
{
    public List<Technology> technologies;
    public Transform panelParent;
    public Image researchBackground;
    public ResearchDisplay prevDisplay;
   
    public List<TechnologyButton> technologyButtons;

    void Awake()
    {
        GenerateTechPanels();
    }

    private void Start()
    {
        SetPrerequisiteInstances();
    }

    private void GenerateTechPanels()
    {
        foreach (Technology technology in technologies)
        {
            TechnologyButton panel = Instantiate(technology.researchUIPrefab, panelParent).GetComponent<TechnologyButton>();
            panel.technology = technology;
            panel.name = technology.displayName;
        }

        technologyButtons = new List<TechnologyButton>();
        technologyButtons.AddRange(GetComponentsInChildren<TechnologyButton>());
    }

    private void SetPrerequisiteInstances()
    {
        // Check if there is a previous tier
        if (prevDisplay)
        {
            // Loop through all the panels/ buttons in this tier
            foreach (TechnologyButton panel in technologyButtons)
            {
                // Loop through all the prerequisite techs needed to unlock the tech
                foreach (Technology tech in panel.technology.prerequisite)
                {
                    // Loop through all the techs in the previous panel to see if the tech matches with the current prereqs
                    foreach (TechnologyButton prevPanel in prevDisplay.technologyButtons)
                    {
                        // If it matches save the instance to a list so we can access it using the technology line
                        if (prevPanel.technology == tech)
                        {
                            panel.prerequsiteButtons.Add(prevPanel);
                        }
                    }
                        
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
