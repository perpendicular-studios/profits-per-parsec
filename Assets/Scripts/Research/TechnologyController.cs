using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyController : GameController<TechnologyController>
{
    public List<Technology> techListStates;                         //Contains a list of all technologies and their current status
    public List<Technology> unlockedTechList;                       //Only contains a list of unlocked technologies for prerequsite checks
    public int researchSpeed = 100;

    public delegate void technologySync();
    public static event technologySync SyncTech;

    void Awake()
    {
        unlockedTechList = new List<Technology>();
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

    public void Unlock(Technology tech, TechnologyButton button)
    {
        unlockedTechList.Add(tech);
        tech.isLocked = false;
        button.UnlockImage();

        //Update unlockable techs 
        SyncTech?.Invoke();
    }

    // When player clicks on a technology to begin research
    public void StartResearch(Technology technology, TechnologyButton button)
    {
        // If technology is currently locked and prerequisites are satisified 
        if (CanUnlock(technology))
        {
            // Start Research Timer
            Unlock(technology, button);
        }
    }
}
