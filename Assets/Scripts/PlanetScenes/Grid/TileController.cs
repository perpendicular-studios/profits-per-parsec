using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : GameController<TileController>
{
    private Dictionary<Planet, List<SectorTileInfo>> _allTiles;

    public List<SectorTileInfo> GetTileInfoListForPlanet(Planet planet)
    {

        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, List<SectorTileInfo>>();
            
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        return _allTiles[planet];
    }

    public void SaveTileForPlanet(Planet planet, List<SectorTileInfo> tileArray)
    {
        if (_allTiles == null)
        {
            _allTiles = new Dictionary<Planet, List<SectorTileInfo>>();
        }

        if (!_allTiles.ContainsKey(planet))
        {
            _allTiles[planet] = null;
        }

        _allTiles[planet] = tileArray;

    }
}
