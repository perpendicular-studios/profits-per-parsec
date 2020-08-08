using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RocketStatusPanel : MonoBehaviour, IPointerClickHandler
{
    public Text rocketTypeText, rocketStatusText;
    public Image rocketTypeImage;
    public Rocket rocket;
    public RocketActivePanel rocketActivePanel;
    public Color defaultColor = new Color32(154, 154, 154, 255);
    public Color highlightColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {

    }

    public void SetText()
    {
        rocketTypeText.text = rocket.rocketType.ToString() + " Rocket";

        if(rocket.status == RocketStatus.Connection)
        {
            rocketStatusText.text = rocket.planetA.planetName + " to " + rocket.planetB.planetName;
        }
        else
        {
            rocketStatusText.text = rocket.status.ToString();
        }

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Control Click
        if (rocketActivePanel.controlDown)
        {
            if (rocketActivePanel.rocketSelectionList.Contains(rocket))
            {
                //Remove rocket from selection and unhighlight button
                rocketActivePanel.rocketSelectionList.Remove(rocket);
                rocketActivePanel.selectedPanels.Remove(this);
                transform.GetComponent<Image>().color = defaultColor;
                //Debug.Log("Rocket removed");
            }
            else
            {
                AddRocketToSelection();
                rocketActivePanel.selectionIndex = rocketActivePanel.activePanels.FindIndex(x => x == this);
            }
        }
        //Shift Click
        else if (rocketActivePanel.shiftDown)
        {
            //Check if selection list is empty
            if(rocketActivePanel.rocketSelectionList.Count == 0)
            {
                AddRocketToSelection();
                rocketActivePanel.selectionIndex = rocketActivePanel.activePanels.FindIndex(x => x == this);
            }
            //Send index of current panel and highlight panels inbetween
            else
            {
                rocketActivePanel.selectionIndex2 = rocketActivePanel.activePanels.FindIndex(x => x == this);
                rocketActivePanel.SelectMultiple();
            }
        }
        //Regular Click
        else
        {
            rocketActivePanel.ResetSelection();

            AddRocketToSelection();
            rocketActivePanel.selectionIndex = rocketActivePanel.activePanels.FindIndex(x => x == this);
        }
    }

    public void AddRocketToSelection()
    {
        if (!rocketActivePanel.rocketSelectionList.Contains(rocket))
        {
            //Add rocket to selection and highlight button
            rocketActivePanel.rocketSelectionList.Add(rocket);
            rocketActivePanel.selectedPanels.Add(this);
            transform.GetComponent<Image>().color = highlightColor;
            rocketActivePanel.SetButtonStates();
            //Debug.Log("Rocket added " + rocketActivePanel.selectionIndex);
        }
    }

    public void ResetHighlight()
    {
        transform.GetComponent<Image>().color = defaultColor;
    }
}
