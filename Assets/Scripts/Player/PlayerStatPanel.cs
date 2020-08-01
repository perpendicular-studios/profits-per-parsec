using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStatPanel : MonoBehaviour
{
    public Image iconImage;
    public Text valueText;
    public GameObject tooltip;

    public void Awake()
    {
        tooltip.SetActive(false);
        iconImage = GetComponentInChildren<Image>();
        valueText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
        Debug.Log("Hello!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

}
