
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    public GameObject cashPanel;
    public GameObject growthRatePanel;
    public GameObject publicRelationPanel;
    public GameObject governmentSupportPanel;
    public GameObject maxSectorPanel;
    public GameObject rocketsPanel;
    
    public void Update()
    {
        string cashString = PlayerStatController.instance.cash.ToString();
        string growthRateString = PlayerStatController.instance.growthRate.ToString("P");
        string publicRelationString = PlayerStatController.instance.publicRelation.ToString();
        string governmentSupportString = PlayerStatController.instance.governmentSupport.ToString();

        cashPanel.GetComponent<PlayerStatPanel>().valueText.text = cashString;
        growthRatePanel.GetComponent<PlayerStatPanel>().valueText.text = growthRateString;
        publicRelationPanel.GetComponent<PlayerStatPanel>().valueText.text = publicRelationString;
        governmentSupportPanel.GetComponent<PlayerStatPanel>().valueText.text = governmentSupportString;

        if (PlayerStatController.instance.currentPlanet != null)
        {
            maxSectorPanel.SetActive(true);
            int currentSectors = SectorController.instance.GetSectorInfoListForPlanet(PlayerStatController.instance.currentPlanet).Count;
            int maxSectors = PlayerStatController.instance.maxSectors;

            maxSectorPanel.GetComponent<PlayerStatPanel>().valueText.text = $"{currentSectors}/{maxSectors}";
            if(currentSectors >= maxSectors)
            {
                maxSectorPanel.GetComponent<PlayerStatPanel>().valueText.color = Color.red;
            }

            rocketsPanel.GetComponent<PlayerStatPanel>().valueText.text = PlayerStatController.instance.numRockets.ToString();
        }
        else
        {
            maxSectorPanel.SetActive(false);
            rocketsPanel.SetActive(false);
        }

    }


}
