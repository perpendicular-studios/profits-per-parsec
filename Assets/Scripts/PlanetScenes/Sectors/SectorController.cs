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
    public static Action OnSectorDeselectNothing;
    public Tile selectedTile;
    public bool isBuilding;
    
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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (selectedTile != null)
                {
                    if (selectedTile.HasSector())
                    {
                        if (hit.transform.gameObject.layer != LayerMask.NameToLayer("UI"))
                        {
                            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("ActiveBuilding"))
                            {
                                selectedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial =
                                    selectedTile.placedSectorObject.GetComponent<SectorInfo>().defaultSectorMaterial;

                                selectedTile = null;
                    
                                // case where mouse hits something (not sector or UI or tile) (we deselect when the selected tile exists)
                                OnSectorDeselect?.Invoke();
                            }
                        }
                    }
                }
            }
            else
            {
                if (selectedTile != null)
                {
                    if (selectedTile.HasSector())
                    {
                        selectedTile.placedSectorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial =
                            selectedTile.placedSectorObject.GetComponent<SectorInfo>().defaultSectorMaterial;

                        selectedTile = null;
                    }

                    // case where mouse hits nothing (no gui, no object, no tile)
                    // we still want to deselect when we hit nothing, no matter if the selected tile has a sector on it or not
                    OnSectorDeselect?.Invoke();
                    
                    // This is to change the camera when the mouse hits NOTHING
                    OnSectorDeselectNothing?.Invoke();
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

