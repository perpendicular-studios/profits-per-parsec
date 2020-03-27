using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatIcon : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public StatType stat;
    public GameObject tooltip;
    public GameObject page;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(page.GetComponent<AdvisorListHire>() != null)
        {
            page.GetComponent<AdvisorListHire>().SortPanels(stat);
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
