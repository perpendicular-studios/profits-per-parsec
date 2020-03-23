using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchToolTips : MonoBehaviour
{
    //Initialize Variables
    //private Text description;
    //private Text title;
    private RectTransform backgroundPanel;

    void Start()
    {

    }

    public void SetToolTipSize(Text title, Text description)
    {
        backgroundPanel = GetComponent<RectTransform>();
        //Set custom tool tip size based on length of title and description
        float paddingWidth = 256f;
        float paddingHeight = 36f;
        Vector2 backgroundSize = new Vector2(paddingWidth, (title.preferredHeight +
            description.preferredHeight + paddingHeight) * 1.25f);
        backgroundPanel.sizeDelta = backgroundSize;
    }
}
