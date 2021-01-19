using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SectorController : GameController<SectorController>
{
    private Dictionary<Planet, List<Tile>> _allSectors;
    public List<GameObject> rocketBuildings = new List<GameObject>();

    public static Action OnSectorDeselect;
    public Tile selectedTile;

    public void Awake()
    {        
        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<Tile>>();
        }

        DateTimeController.OnDailyTick += UpdateSectorIncome;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))    // is the touch on the GUI
            {
                // GUI Action
                return;
            }
            
            if (selectedTile != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.layer != LayerMask.NameToLayer("ActiveBuilding"))
                    {
                        selectedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial = 
                            selectedTile.placedSectorObject.GetComponent<SectorInfo>().defaultSectorMaterial;

                        selectedTile = null;
                        OnSectorDeselect?.Invoke();
                    }
                }
                else
                {
                    selectedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial = 
                        selectedTile.placedSectorObject.GetComponent<SectorInfo>().defaultSectorMaterial;

                    selectedTile = null;
                    OnSectorDeselect?.Invoke();
                }
            }
        }
    }

    public List<Tile> GetSectorInfoListForPlanet(Planet planet) {

        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<Tile>>();
        }

        if (!_allSectors.ContainsKey(planet))
        {
            _allSectors[planet] = new List<Tile>();
        }

        return _allSectors[planet];
    }


    public void SaveSectorForPlanet(Tile sectorTile)
    {
        if (_allSectors == null)
        {
            _allSectors = new Dictionary<Planet, List<Tile>>();
        }

        if (!_allSectors.ContainsKey(sectorTile.parentPlanet.planet))
        {
            _allSectors[sectorTile.parentPlanet.planet] = new List<Tile>();
        }

        _allSectors[sectorTile.parentPlanet.planet].Add(sectorTile);

    }

    public void UpdateSectorIncome()
    {
        foreach(List<Tile> sectorTileList in _allSectors.Values)
        {
            foreach(Tile tile in sectorTileList)
            {
                PlayerStatController.instance.cash += tile.placedSector.cashPerTick;
            }
        }
    }
}

