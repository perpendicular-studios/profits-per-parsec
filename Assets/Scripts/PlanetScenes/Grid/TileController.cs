using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : GameController<TileController>
{
    private Dictionary<Planet, List<TileInfo>> _allTiles;

    public List<TileInfo> GetTileInfoListForPlanet(Planet planet)
    {

        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, List<TileInfo>>();
            
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        return _allTiles[planet];
    }

    public void SaveTileForPlanet(Planet planet, List<TileInfo> tileArray)
    {
        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, List<TileInfo>>();
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        _allTiles[planet] = tileArray;

    }
}
