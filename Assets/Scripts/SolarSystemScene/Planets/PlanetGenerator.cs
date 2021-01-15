using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlanetGenerator : MonoBehaviour
{
    public PlanetScriptableObject sunCenter;
    public List<PlanetScriptableObject> planets;
    public List<PlanetScriptableObject> moons;
    public GameObject planetModel;
    public List<GameObject> planetModelList;
    public GameObject planetCenter;
    public GameObject sunModel;

    void Awake()
    {
        //Initialize Lists if necessary
        if(PlanetController.instance.planets == null)
        {
            PlanetController.instance.planets = new List<Planet>();
        }
        
        //Add initial planet states from scriptable objects to planet classes in singleton (only occurs once on game launch)
        if (PlanetController.instance.sun == null)
        {
            //Create the sun object first
            Planet sun = new Planet();
            SetInitialPlanetStats(sunCenter, sun);
            PlanetController.instance.sun = sun;

            //Create planets
            foreach (PlanetScriptableObject so in planets)
            {
                Planet p = new Planet();
                SetInitialPlanetStats(so, p);
                PlanetController.instance.planets.Add(p);
            }

            //Create moons
            foreach (PlanetScriptableObject so in moons)
            {
                Planet p = new Planet();
                SetInitialPlanetStats(so, p);
                PlanetController.instance.planets.Add(p);
            }
        }
        
        PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Earth"));
        PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Jupiter"));
    }

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlanets();
    }

    public void SetInitialPlanetStats(PlanetScriptableObject so, Planet planet)
    {
        // Set variables in planet class equal to scriptable object
        planet.planetName = so.planetName;
        planet.order = so.order;
        planet.orbitPathX = so.orbitPathX;
        planet.orbitPathY = so.orbitPathY;
        planet.orbitPathZ = so.orbitPathZ;
        planet.orbitProgress = so.orbitProgress;
        planet.orbitPeriod = so.orbitPeriod;
        planet.orbitActive = so.orbitActive;
        planet.segments = so.segments;
        planet.rotationSpeed = so.rotationSpeed;
        planet.dampAmt = so.dampAmt;
        planet.hasMoon = so.hasMoon;
        planet.isMoon = so.isMoon;
        planet.innerPlanet = so.innerPlanet;
        planet.worldScale = so.worldScale;
        planet.zOffset = so.zOffset;
        planet.planetHexMaterial = so.planetHexMaterial;
        planet.planetPentMaterial = so.planetPentMaterial;
        planet.lockedMaterial = so.lockedMaterial;

        // Setting the correct orbiting object
        if(so.orbiting != null)
        {
            // Check if oribts sun
            if (so.orbiting == sunCenter)
            {
                planet.orbiting = PlanetController.instance.sun;
            }
            // Check if orbits planet
            else if (PlanetController.instance.planets.Exists(t => t.planetName == so.orbiting.planetName))
            {
                planet.orbiting = PlanetController.instance.planets.Find(t => t.planetName == so.orbiting.planetName);
            }
        }
    }

    public void GeneratePlanets()
    {
        GameObject sun;
        Planet sunParent = PlanetController.instance.sun;

        // Generate sun model
        sun = Instantiate(sunModel, new Vector3(0, 0, 0), Quaternion.identity);
        sun.name = "Sun";
        sun.GetComponent<PlanetCenterInfo>().planet = sunParent;

        //Generate planet and moon models
        foreach(Planet p in PlanetController.instance.planets)
        {
            //If the planet is not a moon
            if (!p.isMoon)
            {
                GameObject go = Instantiate(planetCenter, sun.transform);
                go.name = p.planetName;
                SetPlanetPositions(go, p);
                planetModelList.Add(go);

                //Check if planet has a moon
                if (p.hasMoon)
                {
                    //Create moon model
                    GameObject moonObject = Instantiate(planetCenter, go.transform.GetChild(0));
                    Planet moon = PlanetController.instance.planets.Find(t => t.orbiting.planetName == p.planetName);
                    moonObject.name = moon.planetName;
                    SetPlanetPositions(moonObject, moon);
                    planetModelList.Add(moonObject);
                }
            }
        }

    }

    public void SetPlanetPositions(GameObject go, Planet p)
    {
        PlanetSelfRotate rotate = go.GetComponent<PlanetSelfRotate>();
        rotate.dampAmt = p.dampAmt;
        rotate.rotationSpeed = p.rotationSpeed;

        OrbitMotion orbit = go.GetComponent<OrbitMotion>();
        //Sets orbit position to account for time passed while not in solar system scene
        p.orbitProgress = (((float)PlanetController.instance.timePassed / (float)p.orbitPeriod) + p.orbitProgress)%1;
        orbit.orbitProgress = p.orbitProgress;
        orbit.orbitPeriod = p.orbitPeriod;
        orbit.orbitPath.xAxis = p.orbitPathX;
        orbit.orbitPath.yAxis = p.orbitPathY;
        orbit.orbitPath.zAxis = p.orbitPathZ;

        GameObject planetModelInstance = null;
        if (p.isMoon)
        {
            planetModelInstance = Instantiate(planetModel, new Vector3(0, 0, p.zOffset), Quaternion.identity);
            
            planetModelInstance.transform.SetParent(go.transform);
            planetModelInstance.GetComponent<Hexsphere>().setWorldScale(
                p.worldScale /
                p.orbiting.worldScale);
        }
        else
        {
            planetModelInstance = Instantiate(planetModel, new Vector3(0, 0, p.zOffset), Quaternion.identity, go.transform);
        }

        planetModelInstance.GetComponent<Hexsphere>().planet = p;
        planetModelInstance.GetComponent<Hexsphere>().planetID = PlanetController.currentPlanetID++;
        planetModelInstance.GetComponent<Hexsphere>().GroupMaterials_Hex[0] = PlayerStatController.instance.isUnlocked(p) ? p.planetHexMaterial : p.lockedMaterial;
        planetModelInstance.GetComponent<Hexsphere>().GroupMaterials_Pent[0] = PlayerStatController.instance.isUnlocked(p) ? p.planetPentMaterial : p.lockedMaterial;
        
        planetModelInstance.GetComponentInChildren<NavigationManager>().pathRenderer = go.GetComponent<LineRenderer>();
        planetModelInstance.name = $"{p.planetName}Model (Instance)";

        orbit.orbitingObject = planetModelInstance.transform;
        rotate.rotatingObject = planetModelInstance.transform;

        PlanetCenterInfo newPlanet = go.GetComponent<PlanetCenterInfo>();
        newPlanet.planet = p;
    }


    public void SavePositions()
    {
        //Save the planet orbit positions for each planet
        foreach (GameObject go in planetModelList)
        {
            Planet p = PlanetController.instance.planets.Find(t => t.planetName == go.GetComponent<PlanetCenterInfo>().planet.planetName);
            p.orbitProgress = go.GetComponent<OrbitMotion>().orbitProgress;
        }
    }
}
