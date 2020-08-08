using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlanetShortcutPanel : MonoBehaviour, IPointerClickHandler
{
    public Planet planet;
    public Image planetImage;
    public Text planetNameText;
    public PlanetSideDisplay planetSideDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Focus camera on " + planet.planetName);
        planetSideDisplay.cameraController.CenterCameraOnObject(planetSideDisplay.planetGenerator.
            planetModelList.Find(x => x.name == planet.planetName).GetComponentInChildren<SphereCollider>().gameObject);

    }

    public void SetText()
    {
        planetNameText.text = planet.planetName;
        //Also set image
    }


}
