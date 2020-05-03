using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyController : GameController<TechnologyController>
{
    public List<Technology> techListStates;                         //Contains a list of all technologies and their current status
    public List<Technology> unlockedTechList;                       //Only contains a list of unlocked technologies for prerequsite checks
    public Dictionary<Technology, int> techProgress;                //Dictionary containing research progress
    public int researchSpeed;                                       //Amount of research gained per day
    public int currentResearchCounter = 0;                          //Current number of research counts
    public Technology currentTech;                                  //Current technology that is being researched
    public bool currentlyResearching = false;                       //boolean which states whether something is being researched

    public delegate void technologySync();
    public static event technologySync SyncTech;

    void Awake()
    {
        unlockedTechList = new List<Technology>();
        techProgress = new Dictionary<Technology, int>();
    }

    private void OnEnable()
    {
        DateTimeController.OnDailyTick += ResearchCount;
    }

    private void OnDisable()
    {
        DateTimeController.OnDailyTick -= ResearchCount;
    }

    // Update is called once per frame
    void Update()
    {
        researchSpeed = PlayerStatController.instance.researchSpeed;
    }

    public bool CanUnlock(Technology tech)
    {
        // Check if tech is currently unlocked, if it is already unlocked return false
        if (tech.isLocked)
        {
            // Check if tech prerequisties are fulfilled
            foreach (Technology t in tech.prerequisite)
            {
                if(!unlockedTechList.Contains(t))
                {
                    Debug.Log("Prerequisite still locked");
                    return false;
                }
            }
            return true;
        }
        Debug.Log("Already Unlocked");
        return false;
    }

    public void Unlock(Technology tech)
    {
        unlockedTechList.Add(tech);
        tech.isLocked = false;

        //Update unlockable techs and their button icons
        SyncTech?.Invoke();
    }

    // When player clicks on a technology to begin research
    public void StartResearch(Technology technology)
    {
        // If technology is currently locked and prerequisites are satisified 
        if (CanUnlock(technology))
        {
            // Start Research Timer by setting currentlyResearching to true
            currentlyResearching = true;
            currentTech = technology;

            currentResearchCounter = techProgress[currentTech];

            Debug.Log("Researching" + currentTech);
        }
    }

    public void ResearchCount()
    {
        if (currentlyResearching)
        {
            //Decreases the research cost of the current tech with each daily tick
            currentResearchCounter -= researchSpeed;

            //Update dictionary 
            if (currentTech != null)
            {
                techProgress[currentTech] = currentResearchCounter;
            }

            //Upon the research cost reaching zero
            if (currentResearchCounter <= 0)
            {
                //Unlock the technology
                Unlock(currentTech);

                //Reset variables
                currentlyResearching = false;
                currentTech = null;

            }
        }


    }
}
