﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlanetPanel : MonoBehaviour
{
    public GameObject planet;
    public Text displayTitle;
    public Image displayImage;
    public Button button;

    public PlanetDisplay display;

    public void Awake()
    {
        display = GetComponentInParent<PlanetDisplay>();
        button = GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = "Enter Planet";
        UnityAction action = new UnityAction(display.OnClick);
        UnityEventTools.AddPersistentListener(button.onClick, action);
    }

    public void Update()
    {
        if(planet != null)
        {
            displayTitle.text = planet.transform.parent.gameObject.tag;
            displayImage.sprite = Resources.Load<Sprite>($"Sprites/sprite_{planet.tag}");
        }
    }

    public void Enable()
    {
        displayTitle.enabled = true;
        displayImage.enabled = true;
    }

    public void Disable()
    {
        displayTitle.enabled = false;
        displayImage.enabled = false;
    }
}