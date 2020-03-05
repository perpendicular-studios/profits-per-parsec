using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTerrainGrid : MonoBehaviour
{
    public Terrain terrain;
    public string cellName = "Grid Cell";
    public int gridWidth;
    public int gridHeight;
    public int cellSize = 1;
    public float yOffset = 0.5f;
    public bool canPlace;
    public int colliderPadding = 10;

    public GameObject activeBuilding;
    public Material validMaterial, invalidMaterial;

    private GameObject[] cells;
    private float[] heights;

    void OnEnable()
    {
        cells = new GameObject[gridHeight * gridWidth];
        heights = new float[(gridHeight + 1) * (gridWidth + 1)];

        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                cells[z * gridWidth + x] = CreateChild();
                Debug.Log($"Created cell at {x}, {z}");
            }
        }
    }

    void Update()
    {
        if (activeBuilding != null)
        {
            gridWidth = (int)activeBuilding.GetComponent<BoxCollider>().bounds.size.x + colliderPadding;
            gridHeight = (int)activeBuilding.GetComponent<BoxCollider>().bounds.size.z + colliderPadding;

            // Update number of cells on a change to the grid size
            UpdateSize();

            // Update positions of cells depending on position of activeBuilding
            UpdatePosition();

            // Update heights of cells depending on height of terrain
            UpdateHeights();

            // Update individual cell meshes and check whether there is collision between grid and building
            UpdateCells();

            // Update cell materials based on result from UpdateCells()
            UpdateCellMaterial();
        }
        else
        {
            // Destroy cells after object has been placed
            DestroyOldCells();
            this.enabled = false;
        }
    }

    private void DestroyOldCells()
    {
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Destroy(cells[z * gridWidth + x]);
            }
        }
    }

    GameObject CreateChild()
    {
        GameObject go = new GameObject();

        go.name = cellName;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.AddComponent<MeshRenderer>();
        go.AddComponent<MeshFilter>().mesh = CreateMesh();

        return go;
    }

    void UpdateSize()
    {
        int newSize = gridHeight * gridWidth;
        int oldSize = cells.Length;

        if (newSize == oldSize)
            return;

        GameObject[] oldCells = cells;
        cells = new GameObject[newSize];

        if (newSize < oldSize)
        {
            for (int i = 0; i < newSize; i++)
            {
                cells[i] = oldCells[i];
            }

            for (int i = newSize; i < oldSize; i++)
            {
                Destroy(oldCells[i]);
            }
        }
        else if (newSize > oldSize)
        {
            for (int i = 0; i < oldSize; i++)
            {
                cells[i] = oldCells[i];
            }

            for (int i = oldSize; i < newSize; i++)
            {
                cells[i] = CreateChild();
            }
        }

        heights = new float[(gridHeight + 1) * (gridWidth + 1)];
    }

    void UpdatePosition()
    {
        Vector3 position = activeBuilding.GetComponent<Transform>().position;

        position.x -= position.x % cellSize + gridWidth * cellSize / 2;
        position.z -= position.z % cellSize + gridHeight * cellSize / 2;
        position.y = 0;

        transform.position = position;
    }

    void UpdateHeights()
    {
        RaycastHit hitInfo;
        Vector3 origin;

        for (int z = 0; z < gridHeight + 1; z++)
        {
            for (int x = 0; x < gridWidth + 1; x++)
            {
                origin = new Vector3(x * cellSize, 200, z * cellSize);
                Physics.Raycast(transform.TransformPoint(origin), Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"));

                heights[z * (gridWidth + 1) + x] = hitInfo.point.y;
            }
        }
    }

    void UpdateCells()
    {
        bool result = true;
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject cell = cells[z * gridWidth + x];
                MeshRenderer meshRenderer = cell.GetComponent<MeshRenderer>();
                MeshFilter meshFilter = cell.GetComponent<MeshFilter>();

                if (!IsCellValid(x, z)) result = false;
                UpdateMesh(meshFilter.mesh, x, z);
            }
        }
        canPlace = result;
    }

    void UpdateCellMaterial()
    {
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject cell = cells[z * gridWidth + x];
                MeshRenderer meshRenderer = cell.GetComponent<MeshRenderer>();
                meshRenderer.material = canPlace ? validMaterial : invalidMaterial;
            }
        }
    }

    bool IsCellValid(int x, int z)
    {
        RaycastHit hitInfo;
        Vector3 origin = new Vector3(x * cellSize + cellSize / 2, 200, z * cellSize + cellSize / 2);
        Physics.Raycast(transform.TransformPoint(origin), Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings"));

        return hitInfo.collider == null;
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.name = "Grid Cell";
        mesh.vertices = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
        mesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0) };

        return mesh;
    }

    void UpdateMesh(Mesh mesh, int x, int z)
    {
        mesh.vertices = new Vector3[] {
            MeshVertex(x, z),
            MeshVertex(x, z + 1),
            MeshVertex(x + 1, z),
            MeshVertex(x + 1, z + 1),
        };
    }

    Vector3 MeshVertex(int x, int z)
    {
        return new Vector3(x * cellSize, heights[z * (gridWidth + 1) + x] + yOffset, z * cellSize);
    }
}

