using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireAdvisor : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public AdvisorPanel panel;
    public GameObject tooltip;
    public AdvisorListAssign advisorList;
    public void OnPointerClick(PointerEventData eventData)
    {
        advisorList.FireAdvisor(panel);
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
