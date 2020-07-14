﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To populate public fields from unity
[System.Serializable]
public class Planet
{

    public string planetName;
    public int order;

    public int orbitPathX;
    public int orbitPathY;
    public int orbitPathZ;
    public float orbitProgress;
    public int orbitPeriod;
    public bool orbitActive;
    public int segments;

    public bool hasMoon;
    public bool isMoon;
    public bool innerPlanet;
    public int rotationSpeed;
    public int dampAmt;
    public GameObject model;
    public Planet orbiting;

    //Planet Modifiers
    public int researchSpeed;
    public int energy;
    public int tourismRevenue;
    public int espionageSpeed;
    public int espionageSuccessRate;
    public int manufacturingSpeed;
    public int miningProduction;

    public List<Advisor> planetAdvisors;

    public void AddAdvisor(Advisor a)
    {
        if (planetAdvisors == null)
        {
            planetAdvisors = new List<Advisor>();
        }
        planetAdvisors.Add(a);
    }

    public void RemoveAdvisor(Advisor a)
    {
        planetAdvisors.Remove(a);
    }

    public void ApplyAdvisorModifiers(Advisor a)
    {

    }
}
