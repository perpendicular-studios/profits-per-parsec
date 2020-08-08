using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlanetPanel : MonoBehaviour
{
    public GameObject planet;
    public Text displayTitle;
    public Image displayImage;
    public Button button;
    public RocketActivePanel rocketActivePanel;
    public RocketConstructionPanel rocketConstructionPanel;

    public PlanetDisplay display;

    public void Awake() 
    {
        display = GetComponentInParent<PlanetDisplay>();

        //button = GetComponentInChildren<Button>();
        //button.GetComponentInChildren<Text>().text = "Enter Planet";
        UnityAction action = new UnityAction(display.OnClick);
        button.onClick.AddListener(action);
        button.GetComponentInChildren<Text>().text = "Enter Planet";

    }

    public void Update()
    {
        if(planet != null)
        {
            displayTitle.text = planet.GetComponentInParent<PlanetCenterInfo>().planet.planetName;
            displayImage.sprite = Resources.Load<Sprite>($"Images/{planet.GetComponentInParent<PlanetCenterInfo>().planet.planetName}");
        }
    }

    public void Enable()
    {
        //Check if planet is unlocked
        if (PlayerStatController.instance.unLockedPlanets.Find(x => x.planetName == planet.GetComponentInParent<PlanetCenterInfo>().planet.planetName) == null)
        {
            //Hide UI
            button.gameObject.SetActive(false);
            rocketActivePanel.gameObject.SetActive(false);
            rocketConstructionPanel.gameObject.SetActive(false);
        }
        else
        {
            //Show UI
            button.gameObject.SetActive(true);
            rocketActivePanel.gameObject.SetActive(true);
            rocketConstructionPanel.gameObject.SetActive(true);
        }

        displayTitle.enabled = true;
        displayImage.enabled = true;
    }

    public void Disable()
    {
        displayTitle.enabled = false;
        displayImage.enabled = false;
    }
}
