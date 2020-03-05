﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResearchTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public ReserachTabHandler tabHandler;
    public void OnPointerClick(PointerEventData eventData)
    {
        tabHandler.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabHandler.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabHandler.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        tabHandler.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
