using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRocketPanel : MonoBehaviour
{
    public Planet planetA;
    public Planet planetB;
    public GameObject titleText;
    public GameObject profit;
    public Text rocketText;
    public Text rocketCount;
    public Slider connectionSlider;
    public GameObject idleRockets;
    public GameObject constructingRockets;

    // Start is called before the first frame update
    void Start()
    {
        if(planetA != null && planetB != null)
        {
            if (planetA.maxCapacity < planetB.maxCapacity)
            {
                connectionSlider.maxValue = planetA.maxCapacity;
            }
            else
            {
                connectionSlider.maxValue = planetB.maxCapacity;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditRocketValueText(float val)
    {
        rocketCount.text = val.ToString();
    }

    public void SetRocketConnectionValue()
    {
        rocketCount.text = connectionSlider.value.ToString();
    }
}
