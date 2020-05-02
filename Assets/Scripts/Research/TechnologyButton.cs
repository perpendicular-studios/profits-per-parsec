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
        TechnologyController.instance.StartResearch(technology, this);
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
    }

    public void OnDisable()
    {
        TechnologyController.SyncTech -= ShowCanUnlockImage;
    }

    public void Update()
    {
        //Get current research speed
        researchSpeed = PlayerStatController.instance.researchSpeed;
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
        researchCost.text = technology.researchCost.ToString();
        hoverPanel.GetComponent<ResearchToolTips>().SetToolTipSize(displayTitle, displayDescription);
        ShowCanUnlockImage();

        if (!tech.isLocked)
        {
            UnlockImage();
        }
    }

    // Shows that a tech can be unlocked or progress of unlock
    public void ShowCanUnlockImage()
    {
        if (TechnologyController.instance.CanUnlock(technology))
        {
            techIconLocked.sprite = technology.unlockedImage;
            techIconLocked.color = Color.white;
        }

    }

    // Updates tech icon image and changes the color
    public void UnlockImage()
    {
        Debug.Log("Unlocked Image");
        techIconLocked.sprite = technology.unlockedImage;
        techIconLocked.color = Color.blue;
    }
}
