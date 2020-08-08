using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanetSelectionItem : MonoBehaviour,IPointerClickHandler
{
    public Planet planet;
    public PlanetSelectionPanel planetSelectionPanel;
    public Text planetNameText;
    public Image planetImade;
    public bool isMouseOver = true;
    public Color selectedColor = new Color32(150, 150, 150, 255);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText()
    {
        planetNameText.text = planet.planetName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        planetSelectionPanel.ResetHighlight();
        GetComponent<Image>().color = selectedColor;
        planetSelectionPanel.selectedPlanet = planet;
        planetSelectionPanel.TurnOnButton();
    }
}
