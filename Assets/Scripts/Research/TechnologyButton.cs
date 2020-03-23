using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TechnologyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Technology technology;

    public GameObject hoverPanel;
    public Text displayTitle;
    public Text displayDescription;
    public Text researchCost;
    public Image techIconUnlocked;
    public Image techIconLocked;
    public Image panelBackground;
    public GameObject techLine;
    public List<TechnologyButton> prerequsiteButtons;


    private Button button;
    private ResearchDisplay display;

    public delegate void technologyClickHandler(Technology technology, TechnologyButton button);
    public static event technologyClickHandler OnTechClick;

    public delegate void checkTech(Technology technology, TechnologyButton button);
    public static event checkTech CheckUnlock;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClick(); });
        panelBackground = GetComponent<Image>();
        display = GetComponentInParent<ResearchDisplay>();
        hoverPanel.SetActive(false);
        technology.isLocked = true;
    }

    public void Start()
    {
        // Set all variables to equal the scriptable object
        if (technology != null)
        {
            displayTitle.text = technology.displayName;
            displayDescription.text = technology.description;
            techIconLocked.sprite = technology.lockedImage;
            researchCost.text = technology.researchCost.ToString();
            hoverPanel.GetComponent<ResearchToolTips>().SetToolTipSize(displayTitle, displayDescription);
        }
    }

    public void Update()
    {
        CheckUnlock?.Invoke(technology, this);
    }

    // Shows that a tech can be unlocked or progress of unlock
    public void showCanUnlockImage()
    {
        techIconLocked.sprite = technology.unlockedImage;
        techIconLocked.color = Color.white;
    }

    // Updates tech icon image and changes the color
    public void unlockImage()
    {
        Debug.Log("Unlocked Image");
        techIconLocked.sprite = technology.unlockedImage;
        techIconLocked.color = Color.blue;
        techLine.GetComponent<TechnologyLine>().changeColor();
    }

    public void OnClick()
    {
        OnTechClick?.Invoke(technology, this);
    }

}
