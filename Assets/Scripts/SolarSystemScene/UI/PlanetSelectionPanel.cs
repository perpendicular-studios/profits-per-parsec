using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSelectionPanel : MonoBehaviour
{
    public GameObject contentHolder;
    public GameObject planetSelectionPrefab;
    public RocketActivePanel rocketActivePanel;
    public Text titleText;
    public Planet selectedPlanet;
    public List<PlanetSelectionItem> panelList;
    public Button confirmButton;
    public Color notSelectedColor = new Color32(255, 255, 255, 255);
    public Color offColor = new Color32(100, 100, 100, 255);
    public Color onColor = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.onClick.AddListener(() => SendRockets());

        foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
        {
            if (p.planetName != PlayerStatController.instance.currentPlanet.planetName)
            {
                PlanetSelectionItem panel = Instantiate(planetSelectionPrefab, contentHolder.transform).GetComponent<PlanetSelectionItem>();
                panel.planetSelectionPanel = this;
                panel.planet = p;
                panel.SetText();
                panelList.Add(panel);
            }
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf &&
                (!RectTransformUtility.RectangleContainsScreenPoint(contentHolder.GetComponent<RectTransform>(), Input.mousePosition, null) &&
                !RectTransformUtility.RectangleContainsScreenPoint(confirmButton.GetComponent<RectTransform>(), Input.mousePosition, null)))
        {
            ResetHighlight();
            TurnOffButton(confirmButton);
        }

    }

    public void SetMode(int modeSetting)
    {
        confirmButton.onClick.RemoveAllListeners();
        //Transfer Rockets
        if (modeSetting == 0)
        {
            confirmButton.onClick.AddListener(() => SendRockets());
            titleText.text = "Select Destination";
        }
        //Set connections
        else if(modeSetting == 1)
        {
            confirmButton.onClick.AddListener(() => SetConnection());
            titleText.text = "Set Connection";
        }

        TurnOffButton(confirmButton);
    }

    public void ResetHighlight()
    {
        foreach(PlanetSelectionItem panel in panelList)
        {
            panel.GetComponent<Image>().color = notSelectedColor;
        }
    }

    public void SendRockets()
    {
        foreach (Rocket rocket in rocketActivePanel.rocketSelectionList)
        {
            Rocket newRocket = new Rocket();
            newRocket.rocketType = rocket.rocketType;
            newRocket.status = RocketStatus.Idle;
            selectedPlanet.AddRocket(newRocket);
            selectedPlanet.UpdateCurrentRockets();
            PlayerStatController.instance.currentPlanet.currRockets.Remove(rocket);
        }

        foreach (RocketStatusPanel panel in rocketActivePanel.selectedPanels)
        {
            rocketActivePanel.activePanels.Remove(panel);
            Destroy(panel.gameObject);
        }

        ResetHighlight();
        TurnOffButton(confirmButton);
        PlayerStatController.instance.currentPlanet.UpdateCurrentRockets();
        rocketActivePanel.ResetSelection();
        rocketActivePanel.SetButtonStates();
        gameObject.SetActive(false);
    }

    public void SetConnection()
    {
        foreach (Rocket rocket in rocketActivePanel.rocketSelectionList)
        {
            rocket.status = RocketStatus.Connection;
            selectedPlanet.AddRocket(rocket);
            rocket.planetA = PlayerStatController.instance.currentPlanet;
            rocket.planetB = selectedPlanet;
            RocketController.instance.CreateConnection(rocket.planetA.planetName, rocket.planetB.planetName);
        }

        ResetHighlight();
        TurnOffButton(confirmButton);
        PlayerStatController.instance.currentPlanet.UpdateCurrentRockets();
        selectedPlanet.UpdateCurrentRockets();
        rocketActivePanel.UpdateText();
        rocketActivePanel.ResetSelection();
        rocketActivePanel.SetButtonStates();
        gameObject.SetActive(false);
    }

    public void TurnOffButton(Button button)
    {
        button.GetComponent<Image>().color = offColor;
        button.GetComponent<Button>().enabled = false;
    }

    public void TurnOnButton()
    {
        confirmButton.GetComponent<Image>().color = onColor;
        confirmButton.GetComponent<Button>().enabled = true;
    }
}
