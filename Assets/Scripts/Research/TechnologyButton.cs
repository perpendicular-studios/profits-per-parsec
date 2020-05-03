using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TechnologyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Technology technology;
    public List<Technology> ownedTechnologies;
    public int researchSpeed;

    public GameObject hoverPanel;
    public Text displayTitle;
    public Text displayDescription;
    public Text researchCost;
    public Image techIconUnlocked;
    public Image techIconLocked;
    public Image panelBackground;
    public List<TechnologyButton> prerequsiteButtons;
    public List<GameObject> preqrequsiteLines;

    private ResearchDisplay display;

    public void OnPointerClick(PointerEventData eventData)
    {
        TechnologyController.instance.StartResearch(technology);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }

    public void OnEnable()
    {
        TechnologyController.SyncTech += ShowCanUnlockImage;
        DateTimeController.OnDailyTick += UpdateTechnologyText;
    }

    public void OnDisable()
    {
        TechnologyController.SyncTech -= ShowCanUnlockImage;
        DateTimeController.OnDailyTick -= UpdateTechnologyText;
    }

    public void Update()
    {
    }

    public void SetTechnology(Technology tech)
    {
        technology = tech;

        // Initialize list if necessary
        if (TechnologyController.instance.techListStates == null)
        {
            TechnologyController.instance.techListStates = new List<Technology>();
        }

        // If technology controller singleton does not contain the current technology add to the list (should only happen on game launch)
        if (!TechnologyController.instance.techListStates.Exists(t => t.displayName == technology.displayName))
        {
            TechnologyController.instance.techListStates.Add(technology);
            TechnologyController.instance.techProgress.Add(technology, technology.researchCost);
            technology.isLocked = true;
        }

        // Mandatory setup tasks
        panelBackground = GetComponent<Image>();
        display = GetComponentInParent<ResearchDisplay>();
        hoverPanel.SetActive(false);

        // Technology Setup
        displayTitle.text = technology.displayName;
        displayDescription.text = technology.description;
        techIconLocked.sprite = technology.lockedImage;
        int researchTime = technology.researchCost;
        if (researchSpeed != 0)
        {
            researchTime /= researchSpeed;
        }

        researchCost.text = researchTime.ToString();
        hoverPanel.GetComponent<ResearchToolTips>().SetToolTipSize(displayTitle, displayDescription);
        ShowCanUnlockImage();

    }

    // Shows that a tech can be unlocked or if it is unlocked
    public void ShowCanUnlockImage()
    {
        if (TechnologyController.instance.CanUnlock(technology))
        {
            techIconLocked.sprite = technology.unlockedImage;
            techIconLocked.color = Color.white;
        }

        if (!technology.isLocked)
        {
            UnlockImage();
        }
    }

    // Updates tech icon image and changes the color
    public void UnlockImage()
    {
        Debug.Log("Unlocked Image");
        techIconLocked.sprite = technology.unlockedImage;
        techIconLocked.color = Color.blue;
    }

    // Controls the text for number of days required for research
    public void UpdateTechnologyText()
    {
        researchSpeed = PlayerStatController.instance.researchSpeed;
        int researchTime = TechnologyController.instance.techProgress[technology];

        // Make sure research speed is not zero
        if (researchSpeed != 0)
        {
            researchTime /= researchSpeed;
        }

        // If research complete show nothing 
        if (researchTime != 0)
        {
            researchCost.text = researchTime.ToString() + " Days";
        }
        else
        {
            researchCost.text = string.Empty;
        }
    }
}
