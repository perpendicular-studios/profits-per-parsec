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
    private bool assignedSort;

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
        panel.GetComponentInChildren<FireAdvisor>().advisorList = this;
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
        assignedSort = false;
    }

    public void SortPanels(StatType stat)
    {
        switch (stat)
        {
            case StatType.Age:
                {
                    if (ageSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.age).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.age).ToList();
                    }
                    ageSort = ChangePanelHierarchy(ageSort, advisorPanels);
                    break;
                }
            case StatType.Knowledge:
                {
                    if (knowledgeSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.knowledge).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.knowledge).ToList();
                    }
                    knowledgeSort = ChangePanelHierarchy(knowledgeSort, advisorPanels);
                    break;
                }
            case StatType.Commerce:
                {
                    if (commerceSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.commerce).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.commerce).ToList();
                    }
                    commerceSort = ChangePanelHierarchy(commerceSort, advisorPanels);
                    break;
                }
            case StatType.Charisma:
                {
                    if (charismaSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.charisma).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.charisma).ToList();
                    }
                    charismaSort = ChangePanelHierarchy(charismaSort, advisorPanels);
                    break;
                }
            case StatType.Engineering:
                {
                    if (engineeringSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.engineering).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.engineering).ToList();
                    }
                    engineeringSort = ChangePanelHierarchy(engineeringSort, advisorPanels);
                    break;
                }
            case StatType.MonthlyCost:
                {
                    if (monthlyCostSort)
                    {
                        advisorPanels = advisorPanels.OrderBy(x => x.advisor.monthlyCost).ToList();
                    }
                    else
                    {
                        advisorPanels = advisorPanels.OrderByDescending(x => x.advisor.monthlyCost).ToList();
                    }
                    monthlyCostSort = ChangePanelHierarchy(monthlyCostSort, advisorPanels);
                    break;
                }
            case StatType.Assigned:
                {
                    advisorPanels = advisorPanels.OrderBy(e => e.isAssigned ? 0 : 1).ToList();
                    assignedSort = ChangePanelHierarchy(assignedSort, advisorPanels);
                    break;
                }
            default:
                break;
        }
    }

    bool ChangePanelHierarchy(bool statBool, List<AdvisorPanel> panels)
    {
        //If clicked only once show from highest to lowest
        if (!statBool)
        {
            statBool = true;
        }
        else
        {
            statBool = false;
        }

        //Check if idle toggle is on
        panels = ToggleIdleShownFirst(panels);

        //Order panels accordingly
        foreach (AdvisorPanel panel in panels)
        {
            panel.transform.SetAsFirstSibling();
        }

        return statBool;
    }

    //Show idle advisors first
    List<AdvisorPanel> ToggleIdleShownFirst(List<AdvisorPanel> panels)
    {
        //If assigned sort bool is already on
        if (assignedSort)
        {
            //Debug.Log("ordering by idle");
            advisorPanels = advisorPanels.OrderBy(x => x.isAssigned ? 0 : 1).ToList();
            return advisorPanels;
        }
        else
        {
            return panels;
        }

    }

    public void FireAdvisor(AdvisorPanel panel)
    {
        advisorPanels.Remove(panel);
        Destroy(panel.gameObject);
        advisorListHirePage.GetComponent<AdvisorListHire>().AddAdvisorFromBeingFired(panel);
    }
}
