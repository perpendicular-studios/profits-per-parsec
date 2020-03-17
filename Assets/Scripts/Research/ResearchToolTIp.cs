using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchToolTip : MonoBehaviour
{
    //Initialize Variables
    public Text description;
    public Text title;
    private RectTransform backgroundPanel;

    void Awake()
    {
        backgroundPanel = GetComponent<RectTransform>();
        //Set custom tool tip size based on length of title and description
        float paddingWidth = 256f;
        float paddingHeight = 36f;
        Vector2 backgroundSize = new Vector2(paddingWidth, title.preferredHeight +
            description.preferredHeight + paddingHeight);
        backgroundPanel.sizeDelta = backgroundSize;
    }
}
