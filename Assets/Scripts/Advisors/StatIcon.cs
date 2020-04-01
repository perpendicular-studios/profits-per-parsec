using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatIcon : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public StatType stat;
    public bool checkmark;
    public Sprite checkmarkImage;
    public Sprite uncheckmarkImage;
    private bool toggled;
    public GameObject tooltip;
    public GameObject page;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(page.GetComponent<AdvisorListHire>() != null)
        {
            page.GetComponent<AdvisorListHire>().SortPanels(stat);
        }
        if(page.GetComponent<AdvisorListAssign>() != null)
        {
            page.GetComponent<AdvisorListAssign>().SortPanels(stat);
        }
        //If stat is the assigned bool
        if (checkmark && !toggled)
        {
            //Show checkmark
            gameObject.GetComponent<Image>().sprite = checkmarkImage;
            toggled = true;
        }
        else if (checkmark && toggled)
        {
            //Unshow checkmark
            gameObject.GetComponent<Image>().sprite = uncheckmarkImage;
            toggled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
