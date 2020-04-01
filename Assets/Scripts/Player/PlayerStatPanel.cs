using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatPanel : MonoBehaviour
{
    public Image iconImage;
    public Text valueText;

    public void Awake()
    {
        iconImage = GetComponentInChildren<Image>();
        valueText = GetComponentInChildren<Text>();
    }
}
