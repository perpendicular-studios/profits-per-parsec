
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    public GameObject cashPanel;
    public GameObject mineralPanel;
    public GameObject growthRatePanel;
    public GameObject publicRelationPanel;
    public GameObject governmentSupportPanel;
    public GameObject maxSectorPanel;
    public GameObject rocketsPanel;

    public void Awake()
    {
        if (PlayerStatController.instance.currentPlanet != null)
        {
            if (!string.IsNullOrEmpty(PlayerStatController.instance.currentPlanet.planetName))
            {
                maxSectorPanel.SetActive(true);
            }
            else
            {
                maxSectorPanel.SetActive(false);
                rocketsPanel.SetActive(false);
            }
        }
        else
        {
            maxSectorPanel.SetActive(false);
            rocketsPanel.SetActive(false);
        }
    }

    public void Update()
    {
        string cashString = PlayerStatController.instance.cash.ToString();
        string mineralString = PlayerStatController.instance.minerals.ToString();
        string growthRateString = PlayerStatController.instance.growthRate.ToString("P");
        string publicRelationString = PlayerStatController.instance.publicRelation.ToString();
        string governmentSupportString = PlayerStatController.instance.governmentSupport.ToString();

        cashPanel.GetComponent<PlayerStatPanel>().valueText.text = cashString;
        mineralPanel.GetComponent<PlayerStatPanel>().valueText.text = mineralString;
        growthRatePanel.GetComponent<PlayerStatPanel>().valueText.text = growthRateString;
        publicRelationPanel.GetComponent<PlayerStatPanel>().valueText.text = publicRelationString;
        governmentSupportPanel.GetComponent<PlayerStatPanel>().valueText.text = governmentSupportString;

        if (PlayerStatController.instance.currentPlanet != null)
        {
            int currentSectors = SectorController.instance.GetSectorInfoListForPlanet(PlayerStatController.instance.currentPlanet).Count;
            int maxSectors = PlayerStatController.instance.maxSectors;

            maxSectorPanel.GetComponent<PlayerStatPanel>().valueText.text = $"{currentSectors}/{maxSectors}";
            if(currentSectors >= maxSectors)
            {
                maxSectorPanel.GetComponent<PlayerStatPanel>().valueText.color = Color.red;
            }

            rocketsPanel.GetComponent<PlayerStatPanel>().valueText.text = PlayerStatController.instance.numRockets.ToString();
        }
    }


}
