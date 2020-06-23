using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdvisorPanel : MonoBehaviour
{
    public Advisor advisor;
    public Image advisorImage;
    public Text displayName;
    public Text age;
    public Text cost;
    public Text monthlyCost;
    public Text knowledge;
    public Text commerce;
    public Text charisma;
    public Text engineering;
    public bool isAssigned;

    public Dropdown dropdown;
    public Image panelBackground;
    //private AdvisorDisplay display;

    public void Awake()
    {
        panelBackground = GetComponent<Image>();
        isAssigned = false;
        //display = GetComponentInParent<AdvisorDisplay>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (advisor != null)
        {
            // Set variables equal to scriptable object
            advisorImage.sprite = advisor.advisorImage;
            displayName.text = advisor.displayName.ToString();
            age.text = advisor.age.ToString();
            knowledge.text = advisor.knowledge.ToString();
            commerce.text = advisor.commerce.ToString();
            charisma.text = advisor.charisma.ToString();
            engineering.text = advisor.engineering.ToString();
            monthlyCost.text = advisor.monthlyCost.ToString();

            if (cost != null)
            {
                cost.text = advisor.cost.ToString();
            }


            if (dropdown != null)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownValueChangedHandler(dropdown); });
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Destroy()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }

    private void DropdownValueChangedHandler(Dropdown target)
    {
        if (target.value == 0)
        {
            {
                isAssigned = false; isAssigned = false;

                //Remove advisor from current planet
                if (advisor.workingPlanet != null)
                {
                    advisor.workingPlanet.RemoveAdvisor(advisor);
                    advisor.workingPlanet = null;
                }
            }
        }
        else
        {
            {
                isAssigned = true; isAssigned = true;


                //Assign advisor to planet here	            //Remove advisor from current planet
                if (advisor.workingPlanet != null)
                {
                    advisor.workingPlanet.RemoveAdvisor(advisor);
                    advisor.workingPlanet = null;
                }

                //Assign advisor to new planet
                Planet p = PlanetController.instance.planets.Find(t => t.planetName == target.options[target.value].text);
                p.AddAdvisor(advisor);
                advisor.workingPlanet = p;
            }
        }
        Debug.Log("selected: " + target.options[target.value].text);
    }
}
