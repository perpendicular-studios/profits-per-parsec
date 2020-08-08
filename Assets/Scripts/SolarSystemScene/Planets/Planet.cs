
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public enum RocketType
{
    Colonial = 0,
    Trade = 1,
    Cargo = 2,
    Tourist = 3
}

public enum RocketStatus
{
    Idle = 0,
    Connection = 1
}

public class RocketQueueItem
{
    public RocketType rocketType;
    public int constructionTime;
}

public class Rocket
{
    public RocketType rocketType;
    public RocketStatus status;
    public Planet planetA;
    public Planet planetB;
    public bool hasConnection;
    public int carryValue;
}

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

    //Planet Rockets
    public int constructingRockets = 0;
    public int idleRockets = 0;                              //rockets that are not in service
    public int currConnections = 0;                          //rockets that are in service and travel to and from the planet
    public int maxCapacity = 5;                              //maximum number of rockets that can be on a planet (maxCapacity >= idleRockets + currConnections + constructingRockets)
    public List<RocketQueueItem> rocketConstructionQueue;    //Queue of current rockets under construction
    public List<Rocket> currRockets;                         //Current rockets in planet

    public List<Advisor> planetAdvisors;

    public bool isRocketCapacityFull()
    {
        return !(maxCapacity > idleRockets + currConnections + constructingRockets);
    }

    public void UpdateCurrentRockets()
    {
        //Update logic later
        idleRockets = currRockets.FindAll(x => x.status == RocketStatus.Idle).Count;
        currConnections = currRockets.FindAll(x => x.status == RocketStatus.Connection).Count;
    }

    public void AddRocket(Rocket rocket)
    {
        currRockets.Add(rocket);
    }

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
