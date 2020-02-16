using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shows a physical path of the ellipse
[RequireComponent(typeof(LineRenderer))]
public class OrbitMotion : MonoBehaviour
{
    // Initiate Object Transfrom and Ellipse 
    public Transform orbitingObject;
    public Ellipse orbitPath;

    // Initialize variables
    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    // Sets number of segments that create the ellipse path
    LineRenderer lr;
    [Range(3, 72)]
    public int segments;

    // Called before start
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    // Draws each segment in the ellipse
    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];

        // Draws out each segment of the ellipse
        for (int i = 0; i < segments; i++)
        {
            Vector2 position2D = orbitPath.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, 0f, position2D.y);
        }
        points[segments] = points[0];

        // Sets Line Renderer 
        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void OnValidate()
    {
        if (Application.isPlaying && lr != null)
        {
            CalculateEllipse();
        }

    }

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y); 
    }

    IEnumerator AnimateOrbit()
    {
        if(orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;
        while(orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
