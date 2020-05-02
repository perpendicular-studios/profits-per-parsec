using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyLine : MonoBehaviour
{
    public ResearchDisplay display;
    private Vector3 pointA;
    private Vector3 pointB;
    public GameObject imageRectTransformPrefab;

    private bool ranOnce = false;

    // Start is called before the first frame update
    void Awake()
    {
        display = GetComponentInParent<ResearchDisplay>();
    }

    // Place in update because the line generation must happen after the intial instantiation
    void Update()
    {
        // Only need to run this code once
        if (!ranOnce)
        {
            CreateLine();
            ranOnce = true;
        }
        UpdateColor();
    }

    public void CreateLine()
    {

        // Loop through each prerequisite tech from the previous tier
        foreach (TechnologyButton button in display.technologyButtons)
        {
            foreach(TechnologyButton prereq in button.prerequsiteButtons)
            {
                pointA = button.transform.position;

                // Instantiate a new image line
                GameObject go = Instantiate(imageRectTransformPrefab, prereq.transform);
                pointB = prereq.transform.position;

                // Find relative distance between prev tech and new tech
                pointB += new Vector3(30, 0, 0);
                pointA += new Vector3(-30, 0, 0);
                Vector3 differenceVector = pointB - pointA;
                //differenceVector += new Vector3(30, 0, 0);

                // Set image position and rotation
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(differenceVector.magnitude, 4);
                go.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
                go.GetComponent<RectTransform>().position = pointA;

                float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
                go.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, angle);

                // Add the line to a list
                button.preqrequsiteLines.Add(go);
            }

        }
    }

    // Changes the color of the line when the tech is unlocked
    public void UpdateColor()
    {
        foreach(TechnologyButton button in display.technologyButtons)
        {
            if (!button.technology.isLocked)
            {
                foreach (GameObject go in button.preqrequsiteLines)
                {
                    go.GetComponent<RectTransform>().GetComponent<Image>().color = UnityEngine.Color.blue;
                    Debug.Log("change to blue");


                }
            }
        }
    }
}
