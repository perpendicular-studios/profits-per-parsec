using System.Collections;
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
    Price
}

public class AdvisorListHire : MonoBehaviour
{
    public AdvisorNames firstName;
    public AdvisorNames lastName;
    public Transform panelParent;
    public AdvisorIcons advisorIcons;
    public GameObject advisorPanelPrefab;

    public int advisorHireLimit = 5;                         //Limit of advisor choices that the player can choose from

    public List<Advisor> advisorListBacklog;                 //List of advisors with custom advisors and randomly generated advisors, which the currentHireList will draw from
    public List<Advisor> currentHireList;                    //In game list of advisors that the player will be able to hire
    public List<AdvisorPanel> advisorPanels;                 //List of currently shown advisors

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
        ResetSorts();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generates advisors
        for(int i = 0; i < 10; i++)
        {
            GenerateRandomAdvisor();
        }

        SelectAdvisors(advisorHireLimit);

        foreach(Advisor a in currentHireList)
        {
            AdvisorPanel panel = Instantiate(advisorPanelPrefab, panelParent).GetComponent<AdvisorPanel>();
            panel.advisor = a;
            //Names the instantiated game object
            panel.name = a.displayName;
        }

        UpdatePanels();

        //Update advisor panel list
        advisorPanels = new List<AdvisorPanel>();
        advisorPanels.AddRange(GetComponentsInChildren<AdvisorPanel>());
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
        newAdvisor.displayName = GenerateAdvisorName(firstName.advisorNames, lastName.advisorNames);
        newAdvisor.advisorImage = advisorIcons.icons[Random.Range(0, advisorIcons.icons.Count)];
        newAdvisor.age = Random.Range(25, 50);
        newAdvisor.knowledge = Random.Range(1, 10);
        newAdvisor.commerce = Random.Range(1, 10);
        newAdvisor.charisma = Random.Range(1, 10);
        newAdvisor.engineering = Random.Range(1, 10);
        newAdvisor.cost = (newAdvisor.knowledge + newAdvisor.commerce + newAdvisor.charisma + newAdvisor.engineering) * 10;
        newAdvisor.monthlyCost = newAdvisor.cost / 5;
        advisorListBacklog.Add(newAdvisor);
    }

    string GenerateAdvisorName(List<string> firstNames, List<string> lastNames)
    {
        string fullName = firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count)];
        return fullName;
    }

    //Choose advisors from the backlog list and add them to the currentList
    void SelectAdvisors(int numAdvisors)
    {
        int random;
        for(int i = 0; i < numAdvisors; i++)
        {
            random = Random.Range(0, advisorListBacklog.Count);
            //Add to current advisor list
            currentHireList.Add(advisorListBacklog[random]);
            //Remove from backlog list
            advisorListBacklog.RemoveAt(random);
        }
    }

    void UpdatePanels()
    {
        int height;
        //Change panel size to fit all panels
        if (currentHireList.Count * 60 + 60 < 400)
        {
            height = 400;
        }
        else
        {
            height = currentHireList.Count * 60 + 60;
        }

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(380, height);

    }

    void ResetPanels()
    {
        advisorPanels.Clear();
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
}
