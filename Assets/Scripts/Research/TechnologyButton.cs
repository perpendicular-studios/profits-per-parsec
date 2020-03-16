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

    private Button button;
    private ResearchDisplay display;

    public delegate void technologyClickHandler(Technology technology);
    public static event technologyClickHandler OnTechClick;

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
        }
    }

    public void OnClick()
    {
        OnTechClick?.Invoke(technology);
    }

}
