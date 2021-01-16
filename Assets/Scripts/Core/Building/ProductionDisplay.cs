using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionDisplay : MonoBehaviour
{
    public Button productionButton;
    public GameObject sectorDisplay;
    
    void Awake() {
        productionButton.onClick.AddListener(OnProductionButtonClick);
    }

    void OnProductionButtonClick()
    {
        sectorDisplay.SetActive(true);
        sectorDisplay.GetComponent<SectorDisplay>().EnableSectorPanels();
    }
}
