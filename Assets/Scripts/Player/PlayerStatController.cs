using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatController : GameController<PlayerStatController>
{
    private int _cash;

    [Range(-1f, 1f)]
    private float _growthRate;

    private int _publicRelation;

    [Range(-100, 100)]
    private int _governmentSupport;

    private int _researchSpeed = 100;

    private int _maxSectors = 3;

    private int _numRockets = 0;

    public int cash { get { return _cash; } set { _cash = value; } }
    public float growthRate { get { return _growthRate; } set { _growthRate = value; } }
    public int governmentSupport { get { return _governmentSupport; } set { _governmentSupport = value; } }
    public int publicRelation { get { return _publicRelation; } set { _publicRelation = value; } }
    public int researchSpeed { get { return _researchSpeed; } set { _researchSpeed = value; } }

    public int maxSectors { get { return _maxSectors; } set { _maxSectors = value; } }

    public int numRockets { get { return _numRockets; } set { _numRockets = value; } }

    public Planet currentPlanet;
    
    public Dictionary<string, CameraInfo> cameraList;

    public List<Planet> unLockedPlanets;                            //List of planets a player has colonized and can send rockets to

    public List<Advisor> advisorListBacklog;                        //List of advisors with custom advisors and randomly generated advisors, which the currentHireList will draw from

    public List<Advisor> advisorHire;                               //List to keep track of advisors available to hire

    public List<Advisor> advisorAssign;                             //List to keep track of hired advisors

    public delegate void UnlockedNewPlanet();
    public static event UnlockedNewPlanet OnPlanetUnlock;
    public delegate void RocketBuilt(Rocket rocket, Planet planet);
    public static event RocketBuilt OnRocketBuilt;

    public void Awake()
    {
        unLockedPlanets = new List<Planet>();
    }

    public bool isUnlocked(Planet planet)
    {
        return unLockedPlanets.Contains(planet.isMoon ? planet.orbiting : planet);
    }
    

    private void OnEnable()
    {
        DateTimeController.OnDailyTick += ConstructRockets;
    }

    private void OnDisable()
    {
        DateTimeController.OnDailyTick -= ConstructRockets;
    }

    public bool CameraExistsForScene(string scene)
    {
        if(cameraList == null)
        {
            cameraList = new Dictionary<string, CameraInfo>();
        }
        
        if(!cameraList.ContainsKey(scene))
        {
            cameraList.Add(scene, new CameraInfo());
            return false;
        }

        return true;
    }

    public void SaveCameraDataForScene(string scene)
    {
        GameObject focus = GameObject.FindGameObjectWithTag("Focus");
        PerspectiveZoomStrategy zoomStrategy = (focus.GetComponent<ProfitsPerParsec.CameraController>().zoomStrategy) as PerspectiveZoomStrategy;
        //Set prev cam info
        CameraInfo prevCameraInfo = new CameraInfo(
                focus.transform.position.x,
                focus.transform.position.y,
                focus.transform.position.z,
                focus.transform.eulerAngles.x,
                focus.transform.eulerAngles.y,
                focus.transform.eulerAngles.z,
                Camera.main.transform.localPosition.x,
                Camera.main.transform.localPosition.y,
                Camera.main.transform.localPosition.z,
                zoomStrategy.currentZoomLevel
                );

        cameraList[SceneManager.GetActiveScene().name] = prevCameraInfo;
    }

    public CameraInfo GetCameraInfoForScene(string scene)
    {

        if (cameraList == null)
        {
            cameraList = new Dictionary<string, CameraInfo>();
        }

        if (!cameraList.ContainsKey(scene))
        {
            cameraList.Add(scene, new CameraInfo());
        }

        return cameraList[scene];
    }

    public void UnlockPlanet()
    {
        OnPlanetUnlock?.Invoke();
    }

    public void ConstructRockets()
    {

        if (unLockedPlanets == null)
        {
            unLockedPlanets = new List<Planet>();
            unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Earth"));
            //PlunLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Venus"));
            /*
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Mars"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Mercury"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Saturn"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Moon"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Jupiter"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.sun);
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Pluto"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Uranus"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Neptune"));
            */
            currentPlanet = PlanetController.instance.planets.Find(x => x.planetName == "Earth");

            foreach(Planet p in unLockedPlanets)
            {
                p.currRockets = new List<Rocket>();
            }

            UnlockPlanet();
        }

        //Loop through player's unlocked planets
        foreach (Planet p in unLockedPlanets)
        {
            //Check if the planet has any queued constructing rockets
            if (p.rocketConstructionQueue != null)
            {
                //Loop through construction queue
                foreach(RocketQueueItem constructingRocket in p.rocketConstructionQueue.ToList())
                {
                    constructingRocket.constructionTime--;

                    //Rocket is finished construction
                    if(constructingRocket.constructionTime <= 0)
                    {
                        p.constructingRockets--;
                        p.idleRockets++;
                        Rocket finishedRocket = new Rocket();
                        finishedRocket.rocketType = constructingRocket.rocketType;
                        finishedRocket.status = RocketStatus.Idle;
                        p.currRockets.Add(finishedRocket);
                        OnRocketBuilt?.Invoke(finishedRocket, p);
                        p.rocketConstructionQueue.Remove(constructingRocket);
                        //Debug.Log("Rocket Built!");
                    }
                }
            }
        }
    }
}
