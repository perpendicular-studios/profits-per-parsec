using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolMode
{
    PaintHeight,
    PaintNavWeight,
    PaintGroupID,
    PaintObject,
    PaintMaterial,
    Select,
}

public enum HeightPaintMode
{
    Add,
    Set
}

public enum ObjectPaintMode
{
    Add,
    Remove
}

public enum NavCostPaintMode
{
    Add,
    Subtract,
    Set
}

public class PlanetTools : MonoBehaviour
{

    [HideInInspector]
    public ToolMode Mode;
    [HideInInspector]
    public float SelectRadius = 0.1f;

    // Height Paint Fields
    [HideInInspector]
    public float ExtrudeHeight;
    [HideInInspector]
    public HeightPaintMode HeightMode;

    // Group Paint Fields
    [HideInInspector]
    public int SelectedGroup;

    // Object Paint Fields
    [HideInInspector]
    public ObjectPaintMode ObjectMode;
    [HideInInspector]
    public List<GameObject> ObjectList = new List<GameObject>();
    [HideInInspector]
    public int SelectedObjectIndex;
    [HideInInspector]
    public float ObjectScale = 1f;

    // Nav path cost paint fields
    [HideInInspector]
    public int SelectedPathCost;
    [HideInInspector]
    public NavCostPaintMode NavCostMode;
}
