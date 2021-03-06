﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum StatType
{
    Age,
    Knowledge,
    Commerce,
    Charisma,
    Engineering,
    MonthlyCost,
    Price,
    Assigned
}

public class AdvisorListHire : MonoBehaviour
{
    public AdvisorNames firstName;
    public AdvisorNames lastName;
    public Transform panelParent;
    public AdvisorIcons advisorIcons;
    public GameObject advisorPanelPrefab;
    public GameObject advisorListInventory;

    public int advisorHireLimit = 5;                         //Limit of advisor choices that the player can choose from

    private List<AdvisorPanel> advisorPanels;                 //In game list of advisors that the player will be able to hire as panels

    //Variables indicating if the category is currently sorted
    private bool ageSort;
    private bool knowledgeSort;
    private bool commerceSort;
    private bool charismaSort;
    private bool engineeringSort;
    private bool monthlyCostSort;
    private bool priceSort;

    private void Awake()
    {
        //Initialize List if necessary
        if (PlayerStatController.instance.advisorHire == null)
        {
            PlayerStatController.instance.advisorHire = new List<Advisor>();
            PlayerStatController.instance.advisorListBacklog = new List<Advisor>();
        }
        advisorPanels = new List<AdvisorPanel>();
        ResetSorts();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generates advisors
        UpdateBackLog();

        //Renews advisors for the player to hire
        RenewAdvisors();

        //Updates the background panel to be large enough to contain all the advisor panels
        UpdatePanels();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        ResetSorts();
    }

    // Generates an advisor with random stats and add them to the backlog list
    public void GenerateRandomAdvisor()
    {
        Advisor newAdvisor = ScriptableObject.CreateInstance<Advisor>();
        newAdvisor.displayName = GenerateAdvisorName(firstName.assetList, lastName.assetList);
        newAdvisor.advisorImage = advisorIcons.assetList[Random.Range(0, advisorIcons.assetList.Count)];
        newAdvisor.age = Random.Range(25, 50);
        newAdvisor.knowledge = Random.Range(1, 10);
        newAdvisor.commerce = Random.Range(1, 10);
        newAdvisor.charisma = Random.Range(1, 10);
        newAdvisor.engineering = Random.Range(1, 10);
        newAdvisor.cost = (newAdvisor.knowledge + newAdvisor.commerce + newAdvisor.charisma + newAdvisor.engineering) * 10;
        newAdvisor.monthlyCost = newAdvisor.cost / 5;
        PlayerStatController.instance.advisorListBacklog.Add(newAdvisor);
    }

    string GenerateAdvisorName(List<string> firstNames, List<string> lastNames)
    {
        string fullName = firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count)];
        return fullName;
    }

    public void AddAdvisorFromBeingFired(AdvisorPanel prevPanel)
    {
        //Create a panel for the advisor and add to panels list
        AdvisorPanel panel = Instantiate(advisorPanelPrefab, panelParent).GetComponent<AdvisorPanel>();
        panel.advisor = prevPanel.advisor;
        panel.name = prevPanel.name;
        panel.GetComponentInChildren<HireAdvisor>().advisorListHirePage = gameObject;
        panel.GetComponentInChildren<RemoveAdvisorCandidate>().advisorList = this;
        advisorPanels.Add(panel);
    }

    //Refills the advisors you can choose to hire from, up to the number a certain limit
    void RenewAdvisors()
    {
        List<Advisor> advisorBackList = PlayerStatController.instance.advisorListBacklog;
        List<Advisor> advisorHireList = PlayerStatController.instance.advisorHire;
        int random;
        int currHireSize = advisorHireList.Count;

        //Create panels for current existing advisors
        foreach (Advisor a in advisorHireList)
        {
            //Create a panel for the advisor and add to panels list
            AdvisorPanel panel = Instantiate(advisorPanelPrefab, panelParent).GetComponent<AdvisorPanel>();
            panel.advisor = a;
            panel.name = a.displayName;
            panel.GetComponentInChildren<HireAdvisor>().advisorListHirePage = gameObject;
            panel.GetComponentInChildren<RemoveAdvisorCandidate>().advisorList = this;
            advisorPanels.Add(panel);
        }

        //Fill up the current hire list if there is still space 
        for (int i = 0; i < advisorHireLimit - currHireSize; i++)
        {
            //Generates a random index 
            random = Random.Range(0, advisorBackList.Count);

            //Create a panel for the advisor and add to panels list
            AdvisorPanel panel = Instantiate(advisorPanelPrefab, panelParent).GetComponent<AdvisorPanel>();
            panel.advisor = advisorBackList[random];
            panel.name = advisorBackList[random].displayName;
            panel.GetComponentInChildren<HireAdvisor>().advisorListHirePage = gameObject;
            panel.GetComponentInChildren<RemoveAdvisorCandidate>().advisorList = this;
            advisorPanels.Add(panel);
            advisorHireList.Add(advisorBackList[random]);

            //Remove from backlog list
            advisorBackList.RemoveAt(random);
        }

        //Update Back Log list if necessary
        UpdateBackLog();
    }

    //Update back log when less than 100 advisors
    void UpdateBackLog()
    {
        if(PlayerStatController.instance.advisorListBacklog.Count < 100)
        {
            for(int i = 0; i < 50; i++)
            {
                GenerateRandomAdvisor();
            }
        }
    }

    //Make sure the size of the scroll window fits to the number of panels
    void UpdatePanels()
    {
        int height;
        //Change panel size to fit all panels
        if (advisorPanels.Count * 70 + 70 < 400)
        {
            height = 400;
        }
        else
        {
            height = advisorPanels.Count * 70 + 70;
        }

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(380, height);

    }

    void ResetPanels()
    {
        advisorPanels.Clear();
    }

    public void HireAdvisor(GameObject advisorPanel)
    {
        //Get the advisor scriptable object that was attached to the Advisor Panel
        Advisor advisor = advisorPanel.GetComponent<AdvisorPanel>().advisor;
        //Set it to a new Advisor Panel in the advisor list assign
        advisorListInventory.GetComponent<AdvisorListAssign>().AddAdvisor(advisor);

        //Remove advisor from this list
        PlayerStatController.instance.advisorHire.Remove(advisor);
        advisorPanels.Remove(advisorPanel.GetComponent<AdvisorPanel>());
        Destroy(advisorPanel);
    }

    public void ResetSorts()
    {
        ageSort = false;
        knowledgeSort = false;
        commerceSort = false;
        charismaSort = false;
        engineeringSort = false;
        monthlyCostSort = false;
        priceSort = false;
    }

    public void SortPanels(StatType stat)
    {
        switch (stat)
        {
            case StatType.Age:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.age).ToList();
                    ageSort = ChangePanelHierarchy(ageSort, advisorPanels);
                    break;
                }
            case StatType.Knowledge: 
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.knowledge).ToList();
                    knowledgeSort = ChangePanelHierarchy(knowledgeSort, advisorPanels);
                    break;
                }
            case StatType.Commerce:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.commerce).ToList();
                    commerceSort = ChangePanelHierarchy(commerceSort, advisorPanels);
                    break;
                }
            case StatType.Charisma:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.charisma).ToList();
                    charismaSort = ChangePanelHierarchy(charismaSort, advisorPanels);
                    break;
                }
            case StatType.Engineering:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.engineering).ToList();
                    engineeringSort = ChangePanelHierarchy(engineeringSort, advisorPanels);
                    break;
                }
            case StatType.MonthlyCost:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.monthlyCost).ToList();
                    monthlyCostSort = ChangePanelHierarchy(monthlyCostSort, advisorPanels);
                    break;
                }
            case StatType.Price:
                {
                    advisorPanels = advisorPanels.OrderBy(x => x.advisor.cost).ToList();
                    priceSort = ChangePanelHierarchy(priceSort, advisorPanels);
                    break;
                }
            default:
                break;
        }
    }

    bool ChangePanelHierarchy(bool statBool, List<AdvisorPanel> panels)
    {
        if (!statBool)
        {
            foreach (AdvisorPanel panel in panels)
            {
                panel.transform.SetAsFirstSibling();
            }
            statBool = true;
        }
        else
        {
            foreach (AdvisorPanel panel in panels)
            {
                panel.transform.SetAsLastSibling();
            }
            statBool = false;
        }
        return statBool;
    }

    public void RemoveCandidate(AdvisorPanel panel)
    {
        advisorPanels.Remove(panel);
        Destroy(panel.gameObject);
    }
}
