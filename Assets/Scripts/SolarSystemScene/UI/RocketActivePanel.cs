using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RocketActivePanel : MonoBehaviour
{
    public GameObject rocketListPanel;
    public GameObject rocketListPanelScrollView;
    public GameObject rocketStatusPanelPrefab;
    public PlanetSelectionPanel planetSelectionPanel;
    public List<RocketStatusPanel> activePanels;
    public List<Rocket> rocketSelectionList;
    public List<RocketStatusPanel> selectedPanels;
    public int selectionIndex = 0;
    public int selectionIndex2 = 0;
    public bool controlDown = false;
    public bool shiftDown = false;
    public Button colonizePlanetButton, transportRocketButton, addConnectionButton, deleteRocketButton;
    public Color offColor = new Color32(100, 100, 100, 255);
    public Color onColor = new Color32(255, 255, 255, 255);

    private void OnEnable()
    {
        PlayerStatController.OnRocketBuilt += AddRocketToList;
    }
    private void OnDisable()
    {
        PlayerStatController.OnRocketBuilt -= AddRocketToList;
    }
    // Start is called before the first frame update
    void Start()
    {
        rocketSelectionList = new List<Rocket>();
        activePanels = new List<RocketStatusPanel>();

        deleteRocketButton.onClick.AddListener(() => DeleteRocket());
        transportRocketButton.onClick.AddListener(() => TransportRockets());
        addConnectionButton.onClick.AddListener(() => AddConnection());

        //Load current rockets
        if (PlayerStatController.instance.currentPlanet.currRockets.Count > 0)
        {
            foreach (Rocket rocket in PlayerStatController.instance.currentPlanet.currRockets)
            {
                //Add rocket to list
                RocketStatusPanel panel = Instantiate(rocketStatusPanelPrefab, rocketListPanel.transform).GetComponent<RocketStatusPanel>();
                panel.rocketActivePanel = this;
                panel.rocket = rocket;
                panel.SetText();
                activePanels.Add(panel);
            }
        }

        SetButtonStates();
    }

    // Update is called once per frame
    void Update()
    {
        //Clear selection if clicked outside rect transform
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf &&
                (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition, null) &&
                !RectTransformUtility.RectangleContainsScreenPoint(planetSelectionPanel.GetComponent<RectTransform>(), Input.mousePosition, null)))
        {
            ResetSelection();
            planetSelectionPanel.gameObject.SetActive(false);
            SetButtonStates();
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            controlDown = true;
        }
        else
        {
            controlDown = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            shiftDown = true;
        }
        else
        {
            shiftDown = false;
        }
    }

    public void AddRocketToList(Rocket rocket, Planet p)
    {
        if(p.planetName == PlayerStatController.instance.currentPlanet.planetName)
        {
            RocketStatusPanel panel = Instantiate(rocketStatusPanelPrefab, rocketListPanel.transform).GetComponent<RocketStatusPanel>();
            panel.rocketActivePanel = this;
            panel.rocket = rocket;
            panel.SetText();
            activePanels.Add(panel);
            SortList();
        }
    }

    public void ResetSelection()
    {
        rocketSelectionList.Clear();
        selectedPanels.Clear();
        foreach (RocketStatusPanel panel in activePanels)
        {
            panel.ResetHighlight();
        }
    }

    public void SelectMultiple()
    {
        if(selectionIndex2 > selectionIndex)
        {
            for (int i = selectionIndex; i <= selectionIndex2; i++)
            {
                activePanels[i].AddRocketToSelection();
            }
        }
        else
        {
            for (int i = selectionIndex; i >= selectionIndex2; i--)
            {
                activePanels[i].AddRocketToSelection();
            }
        }
    }

    public void UpdateText()
    {
        foreach(RocketStatusPanel panel in selectedPanels.ToList())
        {
            panel.SetText();
        }
    }

    public void SortList()
    {
        int[] mapOrder = new[] { 0, 1, 2, 3 };

        activePanels = activePanels.OrderBy(x => mapOrder[(int) (x.rocket.rocketType)]).ToList();
        foreach (RocketStatusPanel panel in activePanels)
        {
            panel.transform.SetAsLastSibling();
        }
    }

    public void SetButtonStates()
    {
        TurnOffButton(colonizePlanetButton);
        TurnOffButton(transportRocketButton);
        TurnOffButton(addConnectionButton);
        TurnOffButton(deleteRocketButton);

        //If selection contains a colonial rocket turn on button 
        if (rocketSelectionList.Find(x => x.rocketType == RocketType.Colonial) != null)
        {
            TurnOnButton(colonizePlanetButton);
        }

        //If selection is not empty turn on transport rocket button 
        if (rocketSelectionList.Count != 0)
        {
            TurnOnButton(transportRocketButton);
            TurnOnButton(deleteRocketButton);
        }

        if(rocketSelectionList.Count != 0 && rocketSelectionList.Find(x => x.rocketType == RocketType.Colonial) == null)
        {
            TurnOnButton(addConnectionButton);
        }
    }

    public void TurnOffButton(Button button)
    {
        button.GetComponent<Image>().color = offColor;
        button.GetComponent<Button>().enabled = false;
    }

    public void TurnOnButton(Button button)
    {
        button.GetComponent<Image>().color = onColor;
        button.GetComponent<Button>().enabled = true;
    }

    public void AddConnection()
    {
        planetSelectionPanel.gameObject.SetActive(true);
        planetSelectionPanel.SetMode(1);
    }

    public void TransportRockets()
    {
        planetSelectionPanel.gameObject.SetActive(true);
        planetSelectionPanel.SetMode(0);
    }

    public void DeleteRocket()
    {
        foreach (Rocket rocket in rocketSelectionList)
        {
            PlayerStatController.instance.currentPlanet.currRockets.Remove(rocket);
        }
        rocketSelectionList.Clear();

        foreach (RocketStatusPanel panel in selectedPanels)
        {
            activePanels.Remove(panel);
            Destroy(panel.gameObject);
        }

        selectedPanels.Clear();

        PlayerStatController.instance.currentPlanet.UpdateCurrentRockets();
        SetButtonStates();
    }

}
