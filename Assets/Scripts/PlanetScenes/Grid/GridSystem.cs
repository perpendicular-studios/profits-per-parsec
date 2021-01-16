using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridSystem : MonoBehaviour
{

    public int height;
    public int width;
    public int tileSize;
    public Material genericMaterial, selectedMaterial;

    public List<SectorTile> tileList;
    public int currTile;

    public static event SectorClicked OnSectorClicked;
    public delegate void SectorClicked(SectorInfo sector);

    public static event SectorDeselect OnSectorDeselect;
    public delegate void SectorDeselect();

    void Awake()
    {
        Planet currentPlanet = PlayerStatController.instance.currentPlanet;
        List<SectorTileInfo> planetTileInfoList = null;
        
        if (currentPlanet != null)
        {
            planetTileInfoList = TileController.instance.GetTileInfoListForPlanet(currentPlanet);
        }
        
        if(planetTileInfoList != null)
        {
            tileList = new List<SectorTile>(new SectorTile[width * height]);

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject newTile = CreateTile(x, z);
                    //Set tileList from singleton values to the new tile

                    SectorTileInfo savedTile = planetTileInfoList[z * height + x];
                    if (savedTile.hasSector)
                    {
                        SectorInfo sectorInfo = newTile.AddComponent<SectorInfo>();
                        sectorInfo.sector = planetTileInfoList[z * height + x].sector;
                        newTile.GetComponent<SectorTile>().sector = sectorInfo;
                    }

                    //Set tileList to attach to the gameobject
                    tileList[z * height + x] = newTile.GetComponent<SectorTile>();
                }
            }
        }
        else
        {
            tileList = new List<SectorTile>(new SectorTile[width * height]);
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject newTile = CreateTile(x, z);
                    tileList[z * height + x] = newTile.GetComponent<SectorTile>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Check if tile is clicked 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                ResetMaterial();
                if (hit.point.x >= 0 && hit.point.z >= 0)
                {
                    int xCol = (int) hit.point.x / tileSize;
                    int zRow = (int) hit.point.z / tileSize;

                    if (xCol < width && zRow < height)
                    {
                        tileList[zRow * height + xCol].gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;

                        //Check if tile has a sector
                        if(tileList[zRow * height + xCol].sector != null)
                        {
                            //Sector info UI stuff here
                            OnSectorClicked?.Invoke(tileList[zRow * height + xCol].sector);
                        }
                        //Tile is not occupied by a sector
                        else
                        {
                            OnSectorClicked?.Invoke(null);
                        }
                        currTile = zRow * height + xCol;
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        tileList.Clear();
    }

    void ResetMaterial()
    {
        foreach(SectorTile tile in tileList)
        {
            tile.gameObject.GetComponent<MeshRenderer>().material = genericMaterial;
        }

        OnSectorDeselect?.Invoke();
    }


    GameObject CreateTile(int x, int z)
    {
        GameObject go = new GameObject();
        int startX = x * tileSize;
        int startZ = z * tileSize;
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(startX, 0, startZ);
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        go.AddComponent<MeshFilter>().mesh = CreateMesh(x, z);
        meshRenderer.material = genericMaterial;
        go.AddComponent<SectorTile>();
        return go;
    }

    Mesh CreateMesh(int x, int z)
    {
        float offsetY = 0.01f;            //to avoid clipping into surface
        Mesh mesh = new Mesh();
        mesh.name = "Tile Cell";
        mesh.vertices = new Vector3[] { new Vector3(0, offsetY, tileSize), new Vector3(tileSize, offsetY, tileSize), 
            new Vector3(0, offsetY, 0), new Vector3(tileSize, offsetY, 0) };
        mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
        mesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0) };

        return mesh;
    }
}
