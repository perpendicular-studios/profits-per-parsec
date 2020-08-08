using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSideDisplay : MonoBehaviour
{
    public GameObject contentHolder;
    public GameObject planetShortcutPrefab;
    public List<PlanetShortcutPanel> panelList;
    public CameraController cameraController;
    public PlanetGenerator planetGenerator;
    public bool selectionMode = false;

    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<PlanetShortcutPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerStatController.OnPlanetUnlock += UpdateList;
    }
    private void OnDisable()
    {
        PlayerStatController.OnPlanetUnlock -= UpdateList;
    }

    public void UpdateList()
    {
        if (panelList != null)
        {
            foreach(PlanetShortcutPanel panel in panelList)
            {
                Destroy(panel.gameObject);
            }
        }

        foreach (Planet p in PlayerStatController.instance.unLockedPlanets)
        {
            PlanetShortcutPanel panel = Instantiate(planetShortcutPrefab, contentHolder.transform).GetComponent<PlanetShortcutPanel>();
            panel.planetSideDisplay = this;
            panel.planet = p;
            panel.SetText();
            panelList.Add(panel);
        }
    }
}
