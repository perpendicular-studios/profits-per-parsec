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
    public Material genericMaterial, selectedMateril;
    public SectorDisplay sectorDisplay;
   
    public Tile[] tileList;
    public GameObject[] objectList;
    public int currTile;
    public int currX;
    public int currZ;
    private bool mouseOff;

    void Awake()
    {
        objectList = new GameObject[width * height];

        Planet currentPlanet = PlayerStatController.instance.currentPlanet;

        if (currentPlanet != null)
        {
            tileList = TileController.instance.GetTileInfoListForPlanet(currentPlanet);

        }
        if(tileList != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject newTile = CreateTile(x, z);
                    objectList[z * height + x] = newTile;
                    newTile.GetComponent<TileManager>().tile = tileList[z * height + x];
                    newTile.GetComponent<TileManager>().grid = this;
                }
            }
        }
        else
        {
            tileList = new Tile[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject newTile = CreateTile(x, z);
                    objectList[z * height + x] = newTile;
                    tileList[z * height + x] = new Tile();
                    newTile.GetComponent<TileManager>().tile = tileList[z * height + x];
                    newTile.GetComponent<TileManager>().grid = this;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetMouseButton(0))
        {
            mouseOff = true;
        }

        //Check if tile is clicked 
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && mouseOff)
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
                        objectList[zRow * height + xCol].GetComponent<MeshRenderer>().material = selectedMateril;

                        //Check if tile has a sector
                        if(objectList[zRow * height + xCol].GetComponent<TileManager>().tile.hasSector)
                        {
                            //Sector UI stuff here
                        }
                        //Tile is not occupied by a sector
                        else
                        {
                            sectorDisplay.EnableSectorPanels();
                        }
                        currTile = zRow * height + xCol;
                    }
                }
            }

            mouseOff = false;
        }
    }

    void ResetMaterial()
    {
        foreach(GameObject go in objectList)
        {
            go.GetComponent<MeshRenderer>().material = genericMaterial;
        }
        sectorDisplay.DisableSectorPanels();
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
        go.AddComponent<TileManager>();
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
