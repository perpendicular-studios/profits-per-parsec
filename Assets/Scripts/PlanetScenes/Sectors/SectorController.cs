using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SectorController : GameController<SectorController>
{
    private Dictionary<Planet, List<Tile>> _allSectors;
    public List<GameObject> rocketBuildings = new List<GameObject>();

    public static Action OnSectorDeselectNothing;
    public Tile selectedTile;
    public bool isBuilding;

    public bool isSelectingRocketDestination;
    
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
        // Deselection logic for when user hits nothing
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSelectingRocketDestination)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (!Physics.Raycast(ray, out hit))
                    {
                        if (selectedTile != null)
                        {
                            if (selectedTile.HasSector())
                            {
                                selectedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial =
                                    selectedTile.placedSectorObject.GetComponent<SectorInfo>().defaultSectorMaterial;

                                selectedTile = null;
                            }

                            // This is to change the camera when the mouse hits NOTHING
                            OnSectorDeselectNothing?.Invoke();
                        }
                    }
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

