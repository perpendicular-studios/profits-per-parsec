using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericTabHandler : MonoBehaviour
{
    public List<Tab> tabs;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public Tab selectedTab;
    public List<GameObject> pagesToSwap;

    // Start is called before the first frame update
    void Start()
    {
        OnTabSelected(selectedTab);
    }

    public void Subscribe(Tab button)
    {
        if (tabs == null)
        {
            tabs = new List<Tab>();
        }

        tabs.Add(button);
    }
    public void OnTabEnter(Tab button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.GetComponent<Image>().color = tabHover;
        }
    }

    public void OnTabExit(Tab button)
    {
        ResetTabs();
    }

    public void OnTabSelected(Tab button)
    {
        selectedTab = button;
        ResetTabs();
        button.GetComponent<Image>().color = tabActive;

        // Make sure the indexes in the page area match up with the indexes in the tab area
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pagesToSwap.Count; i++)
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
        foreach (Tab button in tabs)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.GetComponent<Image>().color = tabIdle;
        }
    }
}
