using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SectorPanel : MonoBehaviour
{
    
    public Sector sector;
    public Text displayTitle;
    public Text displayDescription;
    public Image sectorImage;
    public Image panelBackground;

    private Button button;
    private SectorDisplay display;

    public static event SectorClickHandler OnSectorClick;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClick(); });
        panelBackground = GetComponent<Image>();
        display = GetComponentInParent<SectorDisplay>();
    }

    public void Update()
    {
        if (sector != null)
        {
            displayTitle.text = sector.displayName;
            displayDescription.text = sector.description;
            sectorImage.sprite = sector.image;
        }
    }

    public void OnClick()
    {
        OnSectorClick?.Invoke(sector);
        display.DisableSectorPanels();
    }

    public delegate void SectorClickHandler(Sector sector);
}
