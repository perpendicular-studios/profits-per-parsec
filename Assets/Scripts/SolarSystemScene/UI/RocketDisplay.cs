using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RocketDisplay : MonoBehaviour
{
    public List<GameObject> planetModelPrefabs;

    //Panels
    public GameObject centerPanel;
    public GameObject centerInfoPanel;
    public GameObject leftPanel;
    public GameObject rightPanel;
    public GameObject emptyPanel;
    public GameObject page;
    public GameObject leftPanel2;
    public GameObject rightPanel2;
    public GameObject page2;
    public GameObject connectionLine;

    public int centerScale;
    public int sideScale;
    public int navScale;

    private List<GameObject> panelList;
    private List<GameObject> navPanelList;
    private GameObject centerModel;
    private GameObject centerPanelRef;

    public GameObject morePlanetsButton;
    public GameObject planetRocketPanel;
    public GameObject navPanel;

    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<GameObject>();
        navPanelList = new List<GameObject>();
        if (PlayerStatController.instance.unLockedPlanets == null)
        {
            PlayerStatController.instance.unLockedPlanets = new List<Planet>();
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Earth"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Mars"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Venus"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Mercury"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Saturn"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Moon"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Jupiter"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.sun);
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Pluto"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Uranus"));
            PlayerStatController.instance.unLockedPlanets.Add(PlanetController.instance.planets.Find(x => x.planetName == "Neptune"));
            PlanetController.instance.currentPlanet = PlanetController.instance.planets.Find(x => x.planetName == "Earth");
        }
        SetCenterInfoPanel();
        SetNavigationBar();
        SetModelPositions();
    }

    public void ChangeCenterPlanet(Planet p)
    {
        PlanetController.instance.currentPlanet = p;
        ClearCenter();
        SetCenterInfoPanel();
        SetNavigationBar();
        SetModelPositions();
    }

    public void ClearCenter()
    {
        Destroy(centerModel);

        Destroy(centerPanelRef);
    }

    public void SetCenterInfoPanel()
    {
        GameObject centerPrefab = planetModelPrefabs.Find(x => x.name == PlanetController.instance.currentPlanet.planetName + "Model");
        centerModel = SetModelAsChild(centerPrefab, centerPanel, centerScale);
        centerPanel.GetComponentInChildren<Text>().text = PlanetController.instance.currentPlanet.planetName;
        centerPanelRef = Instantiate(planetRocketPanel, centerInfoPanel.transform);
        centerPanelRef.GetComponent<PlanetRocketPanel>().titleText.GetComponent<Text>().text = PlanetController.instance.currentPlanet.planetName;
    }

    public void SetNavigationBar()
    {
        //Clear Navigation Bar
        foreach(GameObject panel in navPanelList)
        {
            Destroy(panel);
        }

        //Order Planets
        PlayerStatController.instance.unLockedPlanets = PlayerStatController.instance.unLockedPlanets.OrderBy(x => x.order).ToList();

        foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
        {
            GameObject panel = Instantiate(emptyPanel, navPanel.transform);
            panel.GetComponentInChildren<Text>().text = p.planetName;
            panel.GetComponentInChildren<Text>().fontSize = 14;
            panel.GetComponentInChildren<Text>().transform.localPosition = new Vector3(0, -30, 0);
            panel.GetComponent<Button>().onClick.AddListener(() => ChangeCenterPlanet(p));
            GameObject prefab = planetModelPrefabs.Find(x => x.name == p.planetName + "Model");
            GameObject model = SetModelAsChild(prefab, panel, navScale);
            navPanelList.Add(panel);
        }
    }

    public void SetModelPositions()
    {
        ClearModels();

        //Order Planets
        PlayerStatController.instance.unLockedPlanets = PlayerStatController.instance.unLockedPlanets.OrderBy(x => x.order).ToList();

        int numPlanets = PlayerStatController.instance.unLockedPlanets.Count;
        int count = 0;
        int countPage1 = 0;
        int countPage2 = 0;

        //For 2,3,4 planets unlocked
        if (numPlanets >= 2 && numPlanets <= 4)
        {
            foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
            {
                if (p.planetName != PlanetController.instance.currentPlanet.planetName)
                {
                    CreatePanel(rightPanel.transform, p, count, true);
                    count++;
                }

            }
        }
        //For 5,6,7 planets unlocked
        else if (numPlanets >= 5 && numPlanets <= 7)
        {
            foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
            {
                if (p.planetName != PlanetController.instance.currentPlanet.planetName)
                {
                    if (count < 3)
                    {
                        CreatePanel(rightPanel.transform, p, count, true);
                        count++;
                    }
                    else if (count < 6)
                    {
                        CreatePanel(leftPanel.transform, p, count, false);
                        count++;
                    }

                }

            }
        }
        //For 8+ planets unlocked
        else 
        {
            foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
            {
                if (p.planetName != PlanetController.instance.currentPlanet.planetName)
                {
                    if (p.innerPlanet)
                    {
                        if (countPage1 < 3)
                        {
                            CreatePanel(rightPanel.transform, p, countPage1, true);
                            countPage1++;
                        }
                        else
                        {
                            CreatePanel(leftPanel.transform, p, countPage1, false);
                            countPage1++;
                        }
                    }
                    else
                    {
                        if (countPage2 < 3)
                        {
                            CreatePanel(rightPanel2.transform, p, countPage2, true);
                            countPage2++;
                        }
                        else
                        {
                            CreatePanel(leftPanel2.transform, p, countPage2, false);
                            countPage2++;
                        }
                    }

                }

            }
            SetMorePlanetsButton();
        }
        
    }

    private void SetMorePlanetsButton()
    {
        morePlanetsButton.SetActive(true);
    }

    public void TogglePages() 
    {
        if (page.activeSelf)
        {
            page.SetActive(false);
            page2.SetActive(true);
        }
        else
        {
            page2.SetActive(false);
            page.SetActive(true);
        }
    }

    private void CreatePanel(Transform panelTransform, Planet p, int count, bool isRight)
    {
        GameObject panel = Instantiate(emptyPanel, panelTransform);
        panel.GetComponentInChildren<Text>().text = p.planetName;
        panel.GetComponent<Button>().onClick.AddListener(() => ChangeCenterPlanet(p));
        GameObject rocketPanel = Instantiate(planetRocketPanel, panel.transform);
        if (isRight)
        {
            rocketPanel.transform.localPosition = new Vector3(325, 0, 0);
        }
        else
        {
            rocketPanel.transform.localPosition = new Vector3(-325, 0, 0);
        }
        //Set planet rocket panels
        rocketPanel.GetComponent<PlanetRocketPanel>().titleText.GetComponent<Text>().text = PlanetController.instance.currentPlanet.planetName + " to " + p.planetName;
        GameObject prefab = planetModelPrefabs.Find(x => x.name == p.planetName + "Model");
        GameObject model = SetModelAsChild(prefab, panel, sideScale);
        panelList.Add(panel);
        AddLine(panel, count % 3);
    }

    public GameObject SetModelAsChild(GameObject prefab, GameObject panel, int scale) 
    {
        GameObject model = Instantiate(prefab, panel.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = new Vector3(scale, scale, scale);
        model.layer = 12;
        model.tag = "Untagged";

        //model.transform.localEulerAngles = new Vector3(0, 0, 0);
        PlanetSelfRotate rotateScript = model.AddComponent<PlanetSelfRotate>();
        rotateScript.rotationSpeed = 1;
        rotateScript.dampAmt = 2;
        rotateScript.rotatingObject = model.transform;
        return model;
    }

    public void AddLine(GameObject object1, int num) 
    {
        // Instantiate a new image line
        GameObject go = Instantiate(connectionLine, object1.transform);
        float offsetY = - rightPanel.GetComponent<GridLayoutGroup>().cellSize.y - rightPanel.GetComponent<GridLayoutGroup>().spacing.y;
        offsetY *= num;
        Vector3 pointA = transform.InverseTransformPoint(object1.transform.position);
        pointA += new Vector3(0, offsetY, 0);
        Vector3 pointB = Vector3.zero;

        // Find relative distance between the two objects
        Vector3 differenceVector = pointB - pointA;

        // Set image position and rotation
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(differenceVector.magnitude, 2);
        go.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        go.GetComponent<RectTransform>().position = object1.transform.position;

        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        go.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);
    }


    public void ClearModels() 
    {
        foreach(GameObject panel in panelList)
        {
            Destroy(panel);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {

    }
}
