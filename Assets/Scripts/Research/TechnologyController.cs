using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyController : GameController<TechnologyController>
{
    public List<Technology> unlockedTechList;
    public int dataPoints;

    void Awake()
    {
        dataPoints = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        TechnologyButton.OnTechClick += TestUnlock;
    }

    public void OnDisable()
    {
        TechnologyButton.OnTechClick -= TestUnlock;
    }

    public void TestUnlock(Technology tech, TechnologyButton button)
    {
        Debug.Log("Test Unlock");
        // Check if tech can be unlocked
        if(CanUnlock(tech))
        {
            Unlock(tech, button);
            Debug.Log("Unlocked");
        }
    }

    public bool CanUnlock(Technology tech)
    {
        // Check if tech is currently unlocked, if it is already unlocked return false
        if (!unlockedTechList.Contains(tech))
        {
            Debug.Log("Has not been unlocked");
            // Check if tech prerequisties are fulfilled
            foreach (Technology t in tech.prerequisite)
            {
                if(!unlockedTechList.Contains(t))
                {
                    Debug.Log("Prerequisite still locked");
                    return false;
                }
            }

            // Check if player data points are enough to purchase the technology
            if (dataPoints >= tech.researchCost)
            {
                return true;
            }
            Debug.Log("Not enough data points");
            return false;
        }
        Debug.Log("Already Unlocked");
        return false;
    }

    public void Unlock(Technology tech, TechnologyButton button)
    {
        unlockedTechList.Add(tech);
        button.unlockImage();
    }
}
