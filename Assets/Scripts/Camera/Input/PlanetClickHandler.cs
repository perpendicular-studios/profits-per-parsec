using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetClickHandler : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject prevSelectedObject;
    public bool selected = false;
    public bool toggle = false;
    public PlanetDisplay planetDisplay;

    [SerializeField] [Range(0f, 0.5f)]
    public float lerpCount;

    void Awake()
    {

    }

    void OnEnable()
    {
        PlanetDisplay.OnEnterPlanet += EnterPlanet;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If object is hit by mouse
            if (Physics.Raycast(ray, out hit))
            {
                // Check if object has SpaceBody tag
                if (hit.collider.gameObject.tag == "SpaceBody" || hit.collider.gameObject.tag == "Sun")
                {
                    Debug.Log(selectedObject);
                    // If currently selecting object save it so we can reset it
                    if (selectedObject != null)
                    {
                        Debug.Log("Saving Object");
                        prevSelectedObject = selectedObject;
                    }

                    // Get Current SpaceBody object
                    selected = true;
                    selectedObject = hit.collider.gameObject;

                    // Check if SpaceBody is different and reset highlight and lerpCount
                    if (selectedObject != prevSelectedObject && prevSelectedObject != null)
                    {
                        resetObject(prevSelectedObject);
                        lerpCount = 0f;
                        Debug.Log("Resetting Object");
                    }

                    // Place code here when something is selected like displaying menus

                    if (planetDisplay.panel != null)
                    {
                        planetDisplay.DestroyPlanetPanel();
                    }

                    planetDisplay.GeneratePlanetPanel(selectedObject);

                    Debug.Log(hit.collider.gameObject.name);
                }
           
            }
            // Reset selected boolean when you click no object
            else
            {
                //Only if pointer is not over a ui object do we want to do ui deletion
                if(!EventSystem.current.IsPointerOverGameObject())
                {
                    if (planetDisplay.panel != null)
                    {
                        planetDisplay.DestroyPlanetPanel();
                    }
                }

                // Case when you click blank space
                if (selectedObject != null)
                {
                    Debug.Log("Blank");
                    selected = false;
                    resetObject(selectedObject);
                    lerpCount = 0f;
                }
            }
        }

        // While selected make object highlight 
        if (selected)
        {
            cycleLerp();
            cycleColor(selectedObject, lerpCount);
        }

    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void EnterPlanet() {
        if (selectedObject != null)
        {
            SceneManager.LoadScene($"{selectedObject.GetComponent<Transform>().parent.tag}Scene");
        }
    }

    void cycleColor(GameObject selectedObject, float lerpCount)
    {
        if (selectedObject.tag == "Sun")
        {
            selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.yellow, Color.red, lerpCount));
        }
        else
        {
            selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.white, lerpCount));
        }
    }

    void resetObject(GameObject selectedObject)
    {
        if(selectedObject.tag == "Sun")
        {
            selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
        else
        {
            selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }

    }

    void cycleLerp()
    {
        if (lerpCount >= 0.5f)
        {
            toggle = true;
        }
        if (lerpCount <= 0.01f)
        {
            toggle = false;
        }

        if (toggle)
        {
            lerpCount -= 0.01f;
        }
        else
        {
            lerpCount += 0.01f;
        }
    }
}
