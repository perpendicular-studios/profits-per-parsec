using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetDisplay : MonoBehaviour
{
    public GameObject planetUIPrefab;
    public PlanetPanel panel;

    private GameObject instanceUIPrefab;

    public delegate void PlanetUIHandler();
    public static event PlanetUIHandler OnEnterPlanet;

    public void Awake()
    {
        DisablePlanetPanel();
    }

    public void GeneratePlanetPanel(GameObject selectedObject)
    { 
        instanceUIPrefab = Instantiate(planetUIPrefab, transform);
        panel = instanceUIPrefab.GetComponent<PlanetPanel>();
        panel.planet = selectedObject;

        EnablePlanetPanel();
    }

    public void DestroyPlanetPanel()
    {
        Destroy(instanceUIPrefab);
        panel.planet = null;
        DisablePlanetPanel();
    }

    public void EnablePlanetPanel()
    {
        if (panel != null)
        {
            panel.Enable();
        }
    }

    public void DisablePlanetPanel()
    {
        if (panel != null)
        {
            panel.Disable();
        }
    }
    public void OnClick()
    {
        OnEnterPlanet?.Invoke();
    }
}
