using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyLine : MonoBehaviour
{
    public TechnologyButton parent;
    private Technology technology;
    private Vector3 pointA;
    private Vector3 pointB;
    public RectTransform imageRectTransformPrefab;
    public List<RectTransform> techLineList;
    private bool ranOnce = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Place in update because the line generation must happen after the intial instantiation
    void Update()
    {
        // Only need to run this code once
        if (!ranOnce)
        {
            // Set initial variables
            technology = parent.technology;
            pointA = gameObject.transform.position;

            // Loop through each prerequisite tech from the previous tier
            foreach (TechnologyButton panel in parent.prerequsiteButtons)
            {
                // Instantiate a new image line
                RectTransform imageRectTransform = Instantiate(imageRectTransformPrefab, parent.transform).GetComponent<RectTransform>();
                pointB = panel.transform.position;

                // Find relative distance between prev tech and new tech
                Vector3 differenceVector = pointB - pointA;
                differenceVector += new Vector3(30, 0, 0);

                // Set image position and rotation
                imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, 4);
                imageRectTransform.pivot = new Vector2(0, 0.5f);
                imageRectTransform.position = pointA;

                float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
                imageRectTransform.localRotation = Quaternion.Euler(0, 0, angle);

                // Add the line to a list
                techLineList.Add(imageRectTransform);
            }
            ranOnce = true;
        }
        
    }

    // Changes the color of the line when the tech is unlocked
    public void changeColor()
    {
        foreach(RectTransform rt in techLineList)
        {
            rt.GetComponent<Image>().color = Color.blue;
        }
    }
}
