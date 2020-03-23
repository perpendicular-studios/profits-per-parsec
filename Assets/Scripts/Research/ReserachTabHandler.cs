using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReserachTabHandler : MonoBehaviour
{
    public List<ResearchTabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public ResearchTabButton selectedTab;
    public List<GameObject> pagesToSwap;

    //Start with the default selected tab 
    void Start()
    {
        OnTabSelected(selectedTab);
    }

    public void Subscribe(ResearchTabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<ResearchTabButton>();
        }

        tabButtons.Add(button);
    }
    public void OnTabEnter(ResearchTabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.GetComponent<Image>().color = tabHover;
        }
    }

    public void OnTabExit(ResearchTabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(ResearchTabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.GetComponent<Image>().color = tabActive;

        // Make sure the indexes in the page area match up with the indexes in the tab area
        int index = button.transform.GetSiblingIndex();
        for(int i=0; i < pagesToSwap.Count; i++)
        {
            if (i == index)
            {
                pagesToSwap[i].SetActive(true);
            }
            else
            {
                pagesToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(ResearchTabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.GetComponent<Image>().color = tabIdle;
        }
    }
}
