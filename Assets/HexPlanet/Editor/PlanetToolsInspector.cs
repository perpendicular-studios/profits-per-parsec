using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(PlanetTools))]
public class PlanetToolsInspector : Editor
{
    public PlanetTools PlanetEditor;

    public ToolMode Mode;

    private Hexsphere Planet;

    private Tile LastHilightedTile;
    private static GUIStyle ToggleButtonStyleNormal = null;
    private static GUIStyle ToggleButtonStyleToggled = null;

    private bool MouseIsDown;

    // Tiles which have already have the selected action applied on them
    private List<Tile> AppliedActionTiles = new List<Tile>();

    private bool IsEditing;

    // Paint Material Fields
    private Material SelectedMaterial;

    // Paint Group Fields
    private int SelectedGroup;
    private Vector2 GroupScrollPos;

    // Paint Height fields
    private HeightPaintMode HeightMode;
    private float ExtrudeHeight;

    // Paint Object fields
    private ObjectPaintMode ObjectMode;
    private Vector2 ObjectListScrollPos;
    private List<GameObject> ObjectList;
    private int SelectedObjectIndex;

    private TileDisplayOptions SelectedTileInfoOption;

    public override void OnInspectorGUI()
    {
        if(PlanetEditor == null)
        {
            PlanetEditor = target as PlanetTools;
        }

        if(Planet == null)
        {
            Planet = PlanetEditor.GetComponent<Hexsphere>();
        }

        if (ToggleButtonStyleNormal == null)
        {
            ToggleButtonStyleNormal = "Button";
            ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
            ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
        }

        GUILayout.Label("Tile Tools", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.BeginHorizontal();
        
        //if(GUILayout.Button("Select", Mode == ToolMode.Select ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        //{
        //    Mode = ToolMode.Select;
        //}

        //if (GUILayout.Button("Paint Material", Mode == ToolMode.PaintMaterial ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        //{
        //    Mode = ToolMode.PaintMaterial;
        //}

        if (GUILayout.Button("Paint Group ID", PlanetEditor.Mode == ToolMode.PaintGroupID ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.Mode = ToolMode.PaintGroupID;
        }

        if (GUILayout.Button("Paint Height", PlanetEditor.Mode == ToolMode.PaintHeight ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.Mode = ToolMode.PaintHeight;
        }

        if (GUILayout.Button("Paint Object", PlanetEditor.Mode == ToolMode.PaintObject ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.Mode = ToolMode.PaintObject;
        }

        if (GUILayout.Button("Paint Nav Weight", PlanetEditor.Mode == ToolMode.PaintNavWeight ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.Mode = ToolMode.PaintNavWeight;
            SelectedTileInfoOption = TileDisplayOptions.NavWeight;
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        switch(PlanetEditor.Mode)
        {
            case ToolMode.Select:
                break;

            case ToolMode.PaintMaterial:
                DrawPaintMaterialInspector();
                break;

            case ToolMode.PaintHeight:
                DrawPaintHeightInspector();
                break;

            case ToolMode.PaintGroupID:
                DrawPaintGroupInspector();
                break;

            case ToolMode.PaintObject:
                DrawPaintObjectInspector();
                break;

            case ToolMode.PaintNavWeight:
                DrawPaintNavWeightInspector();
                break;
        }
    }

    void DrawPaintHeightInspector()
    {
        GUILayout.Space(5);

        GUILayout.Label("Paint Height Options", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Set Height", PlanetEditor.HeightMode == HeightPaintMode.Set ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.HeightMode = HeightPaintMode.Set;
        }

        if (GUILayout.Button("Set Height All", ToggleButtonStyleNormal))
        {
            for(int i = 0; i < Planet.tiles.Count; i++)
            {
                Planet.tiles[i].SetExtrusionHeight(PlanetEditor.ExtrudeHeight);
            }
        }

        if (GUILayout.Button("Add Height", PlanetEditor.HeightMode == HeightPaintMode.Add ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.HeightMode = HeightPaintMode.Add;
        }

        if (GUILayout.Button("Add Height All", ToggleButtonStyleNormal))
        {
            for (int i = 0; i < Planet.tiles.Count; i++)
            {
                Planet.tiles[i].Extrude(PlanetEditor.ExtrudeHeight);
            }
        }

        EditorGUILayout.EndHorizontal();

        PlanetEditor.ExtrudeHeight = EditorGUILayout.FloatField("Extrude Height: ", PlanetEditor.ExtrudeHeight);

        GUILayout.EndVertical();

        // TODO: Sample Height Tool
    }

    void DrawPaintMaterialInspector()
    {
        GUILayout.Space(5);

        GUILayout.Label("Paint Material Options", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set Height", PlanetEditor.HeightMode == HeightPaintMode.Set ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.HeightMode = HeightPaintMode.Set;
        }
        
        if (GUILayout.Button("Add Height", PlanetEditor.HeightMode == HeightPaintMode.Add ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.HeightMode = HeightPaintMode.Add;
        }
        EditorGUILayout.EndHorizontal();

        SelectedMaterial = EditorGUILayout.ObjectField("Material", SelectedMaterial, typeof(Material), false) as Material;

        GUILayout.EndVertical();
    }

    void DrawPaintGroupInspector()
    {
        GUILayout.Space(5);

        GUILayout.Label("Paint Tile Group Options", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        GroupScrollPos = EditorGUILayout.BeginScrollView(GroupScrollPos, GUILayout.Height(45));
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < Planet.GroupMaterials_Hex.Length; i++)
        {
            if (GUILayout.Button("Group " + i.ToString(), i == PlanetEditor.SelectedGroup ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
            {
                PlanetEditor.SelectedGroup = i;
            }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        

        GUILayout.EndVertical();
    }

    void DrawPaintNavWeightInspector()
    {
        GUILayout.Space(5);

        GUILayout.Label("Paint Tile Nav Weight Options", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add", PlanetEditor.NavCostMode == NavCostPaintMode.Add ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.NavCostMode = NavCostPaintMode.Add;
        }

        if (GUILayout.Button("Subtract", PlanetEditor.NavCostMode == NavCostPaintMode.Subtract ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.NavCostMode = NavCostPaintMode.Subtract;
        }

        if (GUILayout.Button("Set", PlanetEditor.NavCostMode == NavCostPaintMode.Set ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.NavCostMode = NavCostPaintMode.Set;
        }
        EditorGUILayout.EndHorizontal();

        PlanetEditor.SelectedPathCost = EditorGUILayout.IntSlider("Nav Path Weight: ", PlanetEditor.SelectedPathCost, 1, 100);

        GUILayout.EndVertical();
    }

    void DrawPaintObjectInspector()
    {
        GUILayout.Space(5);

        GUILayout.Label("Paint Object Options", EditorStyles.boldLabel);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        // Mode buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Object", PlanetEditor.ObjectMode == ObjectPaintMode.Add ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.ObjectMode = ObjectPaintMode.Add;
        }
        if (GUILayout.Button("Remove Object", PlanetEditor.ObjectMode == ObjectPaintMode.Remove ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            PlanetEditor.ObjectMode = ObjectPaintMode.Remove;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUILayout.Label("Prefab List");
        // Object selector scroll
        ObjectListScrollPos = EditorGUILayout.BeginScrollView(ObjectListScrollPos, EditorStyles.helpBox, GUILayout.Height(75));

        GUIStyle anchorLeft = new GUIStyle();
        anchorLeft.alignment = TextAnchor.MiddleLeft;
        EditorGUILayout.BeginHorizontal(GUILayout.Width(PlanetEditor.ObjectList.Count * 100));
        for (int i = 0; i < PlanetEditor.ObjectList.Count; i++)
        {
            EditorGUILayout.BeginVertical();
            // Object field
            PlanetEditor.ObjectList[i] = EditorGUILayout.ObjectField(PlanetEditor.ObjectList[i], typeof(GameObject), false, GUILayout.Width(100), GUILayout.Height(20)) as GameObject;

            EditorGUILayout.BeginHorizontal();
            // Select Button
            if (GUILayout.Button("Select", i == PlanetEditor.SelectedObjectIndex ? ToggleButtonStyleToggled : ToggleButtonStyleNormal, GUILayout.Width(80), GUILayout.Height(20)))
            {
                PlanetEditor.SelectedObjectIndex = i;
            }

            // Remove slot button
            if(GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                PlanetEditor.ObjectList.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        // Add object slot button
        if(GUILayout.Button("+", GUILayout.Width(40), GUILayout.Height(40)))
        {
            PlanetEditor.ObjectList.Add(null);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        PlanetEditor.ObjectScale = EditorGUILayout.FloatField("Object Scale", PlanetEditor.ObjectScale);

        GUILayout.EndVertical();
    }

    private void OnSceneGUI()
    {
        if(PlanetEditor == null)
        {
            PlanetEditor = target as PlanetTools;
        }

        if(Planet == null)
        {
            Planet = PlanetEditor.GetComponent<Hexsphere>();
        }

        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        // Shift to allow editing
        if (Event.current.shift)
        {
            IsEditing = true;

            if(Event.current.type == EventType.MouseDown)
            {
                MouseIsDown = true;
                GUIUtility.hotControl = controlId;
                // Use the event
                Event.current.Use();
            }
            else if(Event.current.type == EventType.MouseUp)
            {
                MouseIsDown = false;
                GUIUtility.hotControl = 0;
                // Use the event
                Event.current.Use();
            }

            if(Event.current.type == EventType.ScrollWheel && Event.current.control)
            {
                SetSelectCircleRadius(PlanetEditor.SelectRadius + Event.current.delta.y * 0.01f);
                GUIUtility.hotControl = controlId;
                Event.current.Use();
            }
            else
            {
                GUIUtility.hotControl = 0;
            }

            //RaycastSelect();
            CircleSelect();
        }
        // Shift is released
        else if(IsEditing)
        {
            IsEditing = false;

            if (LastHilightedTile != null)
            {
                LastHilightedTile.SetHighlight(false);
                LastHilightedTile = null;
            }

            foreach(Tile lastT in LastHilightedTiles)
            {
                lastT.SetHighlight(false);
            }
            LastHilightedTiles.Clear();

            AppliedActionTiles.Clear();
        }
    }

    List<Tile> LastHilightedTiles = new List<Tile>();

    void RaycastSelect()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Tile t = hit.collider.gameObject.GetComponent<Tile>();
            if (t != null)
            {
                // Unhighlight last tile
                if (LastHilightedTile != null)
                {
                    LastHilightedTile.SetHighlight(false);
                }
                // Hilight current tile
                t.SetHighlight(true);
                LastHilightedTile = t;

                // If the mouse is pressed and we havent already applied the action to this tile
                if (MouseIsDown && !AppliedActionTiles.Contains(t))
                {
                    AppliedActionTiles.Add(t);
                    ApplyToolAction(t);
                }

                Handles.BeginGUI();
                DrawCircleCursor(Color.white, 50f);
                Handles.EndGUI();
            }
        }
        else if (LastHilightedTile != null)
        {
            LastHilightedTile.SetHighlight(false);
            LastHilightedTile = null;
        }
    }

    void SetSelectCircleRadius(float radius)
    {
        if(radius <= 0.001f)
        {
            radius = 0.001f;
        }

        PlanetEditor.SelectRadius = radius;
    }

    void CircleSelect()
    {

        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Tile t = hit.collider.gameObject.GetComponent<Tile>();
            if (t != null)
            {
                // Calc screen circle radius
                Vector3 worldHitExtent = hit.point + SceneView.currentDrawingSceneView.camera.transform.right * PlanetEditor.SelectRadius;
                Vector2 screenCircleEdgePoint = HandleUtility.WorldToGUIPoint(worldHitExtent);
                float screenCircleRadius = (screenCircleEdgePoint - Event.current.mousePosition).magnitude;

                Handles.BeginGUI();
                DrawCircleCursor(Color.white, screenCircleRadius);
                Handles.EndGUI();


                // Unhilight previous tiles
                foreach (Tile lastT in LastHilightedTiles)
                {
                    lastT.SetHighlight(false);
                }
                LastHilightedTiles.Clear();

                // Add raycast hit tile to list
                if (!LastHilightedTiles.Contains(t))
                {
                    t.SetHighlight(true);
                    LastHilightedTiles.Add(t);
                }
                // Overlap sphere on hit tile
                Collider[] sphereHits = Physics.OverlapSphere(hit.point, PlanetEditor.SelectRadius);
                foreach(Collider tCollider in sphereHits)
                {
                    Tile ti = tCollider.GetComponent<Tile>();
                    if (ti != null && !LastHilightedTiles.Contains(ti))
                    {
                        ti.SetHighlight(true);
                        LastHilightedTiles.Add(ti);
                    }
                }

                // If the mouse is pressed and we havent already applied the action to this tile
                if (MouseIsDown)
                {
                    foreach(Tile hilighted in LastHilightedTiles)
                    {
                        if(!AppliedActionTiles.Contains(hilighted))
                        {
                            AppliedActionTiles.Add(hilighted);
                            ApplyToolAction(hilighted);
                        }
                    }
                    
                }
            }
        }
        else
        {
            foreach(Tile t in LastHilightedTiles)
            {
                t.SetHighlight(false);
            }
            LastHilightedTiles.Clear();
        }

        
    }

    void ApplyToolAction(Tile t)
    {
        t.InfoDisplayOption = SelectedTileInfoOption;

        switch(PlanetEditor.Mode)
        {
            case ToolMode.Select:
                break;
            case ToolMode.PaintMaterial:

                t.SetMaterial(SelectedMaterial);
                break;

            case ToolMode.PaintGroupID:
                t.SetGroupID(PlanetEditor.SelectedGroup);
                break;

            case ToolMode.PaintHeight:

                if(PlanetEditor.HeightMode == HeightPaintMode.Add)
                {
                    t.Extrude(PlanetEditor.ExtrudeHeight);
                }
                else if(PlanetEditor.HeightMode == HeightPaintMode.Set)
                {
                    t.SetExtrusionHeight(PlanetEditor.ExtrudeHeight);
                }
                
                break;

            case ToolMode.PaintObject:

                if(PlanetEditor.ObjectMode == ObjectPaintMode.Add)
                {
                    if (PlanetEditor.SelectedObjectIndex < PlanetEditor.ObjectList.Count && PlanetEditor.ObjectList[PlanetEditor.SelectedObjectIndex] != null)
                    {
                        GameObject o = Instantiate(PlanetEditor.ObjectList[PlanetEditor.SelectedObjectIndex]) as GameObject;
                        o.transform.localScale *= PlanetEditor.ObjectScale;
                        t.placeObject(o);
                    }
                }
                else if(PlanetEditor.ObjectMode == ObjectPaintMode.Remove)
                {
                    t.DeleteLastPlacedObject();
                }
                
                break;

            case ToolMode.PaintNavWeight:

                if(PlanetEditor.NavCostMode == NavCostPaintMode.Add)
                {
                    t.pathCost += PlanetEditor.SelectedPathCost;
                }
                else if (PlanetEditor.NavCostMode == NavCostPaintMode.Subtract)
                {
                    t.pathCost -= PlanetEditor.SelectedPathCost;
                }
                else if (PlanetEditor.NavCostMode == NavCostPaintMode.Set)
                {
                    t.pathCost = PlanetEditor.SelectedPathCost;
                }
                break;
        }
    }

    void DrawCircleCursor(Color color, float radius)
    {
        Handles.color = color;
        Handles.CircleHandleCap(0, Event.current.mousePosition, Quaternion.identity, radius, EventType.Repaint);
    }
}
