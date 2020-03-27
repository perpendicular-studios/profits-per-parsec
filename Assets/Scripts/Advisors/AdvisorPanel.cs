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

    public Image panelBackground;
    //private AdvisorDisplay display;

    public void Awake()
    {
        panelBackground = GetComponent<Image>();
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
            cost.text = advisor.cost.ToString();
            monthlyCost.text = advisor.monthlyCost.ToString();
            knowledge.text = advisor.knowledge.ToString();
            commerce.text = advisor.commerce.ToString();
            charisma.text = advisor.charisma.ToString();
            engineering.text = advisor.engineering.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
       
    }
}
