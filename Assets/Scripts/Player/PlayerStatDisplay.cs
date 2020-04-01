
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    public GameObject cashPanel;
    public GameObject growthRatePanel;
    public GameObject publicRelationPanel;
    public GameObject governmentSupportPanel;
    
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
    }


}
