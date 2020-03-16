using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchDisplay : MonoBehaviour
{
    public List<Technology> technologies;
    public Transform panelParent;
    public Image researchBackground;
   
    private List<TechnologyButton> technologyButtons;

    void Awake()
    {
        GenerateBuildingPanels();
    }

    private void GenerateBuildingPanels()
    {
        foreach (Technology technology in technologies)
        {
            TechnologyButton panel = Instantiate(technology.researchUIPrefab, panelParent).GetComponent<TechnologyButton>();
            panel.technology = technology;
        }

        technologyButtons = new List<TechnologyButton>();
        technologyButtons.AddRange(GetComponentsInChildren<TechnologyButton>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
