using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AdvisorListAssign : MonoBehaviour
{
    public Transform panelParent;
    public GameObject advisorListHirePage;
    public GameObject advisorPanelEmployedPrefab;

    public List<AdvisorPanel> advisorPanels;                   //List of currently hired advisors that can be assigned to a planet

    //Variables indicating if the category is currently sorted
    private bool ageSort;
    private bool knowledgeSort;
    private bool commerceSort;
    private bool charismaSort;
    private bool engineeringSort;
    private bool monthlyCostSort;
    private bool priceSort;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDisable()
    {
        ResetSorts();
    }

    public void AddAdvisor(Advisor advisor)
    {
        //Create a panel for the advisor and add to panels list
        AdvisorPanel panel = Instantiate(advisorPanelEmployedPrefab, panelParent).GetComponent<AdvisorPanel>();
        panel.advisor = advisor;
        panel.name = advisor.displayName;
        advisorPanels.Add(panel);
    }

    void UpdatePanels()
    {
        int height;
        //Change panel size to fit all panels
        if (advisorPanels.Count * 60 + 60 < 400)
        {
            height = 400;
        }
        else
        {
            height = advisorPanels.Count * 60 + 60;
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
