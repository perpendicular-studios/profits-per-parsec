using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationDemo : MonoBehaviour
{
    public MobileUnit Unit;
    
	// Use this for initialization
	void Start ()
    {
        Tile.OnTileClickedAction += OnTileClicked;
	}
	

    public void OnTileClicked(Tile tile)
    {
        if(!Unit.moving)
        {
            Stack<Tile> path;
            if(Hexsphere.planetInstances[0].navManager.findPath(Unit.currentTile, tile, out path))
            {
                Unit.moveOnPath(path);
            }
        }
    }
}
