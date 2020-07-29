using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : GameController<TileController>
{
    private Dictionary<Planet, Tile[]> _allTiles;

    public Tile[] GetTileInfoListForPlanet(Planet planet)
    {

        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, Tile[]>();
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        return _allTiles[planet];
    }

    public void SaveTileForPlanet(Planet planet, Tile[] tileArray)
    {
        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, Tile[]>();
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        _allTiles[planet] = tileArray;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
