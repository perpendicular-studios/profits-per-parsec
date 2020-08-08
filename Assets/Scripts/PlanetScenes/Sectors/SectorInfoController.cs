using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorInfoController : MonoBehaviour
{
    public Sector selectedSector;
    public Sector previousSector;
    public GameObject previousSelectedSectorObject;
    public GameObject selectedSectorObject;
    public Material selectedSectorMaterial;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If object is hit by mouse
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (LayerMask.LayerToName(hitObject.layer) == "Sectors")
            {
                SectorInfo sectorInfo = hitObject.GetComponent<SectorInfo>();
                if (selectedSector != null)
                {
                    previousSelectedSectorObject = selectedSectorObject;
                    previousSector = selectedSector;

                    previousSelectedSectorObject.GetComponent<MeshRenderer>().material = previousSector.sectorModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                }

                selectedSectorObject = hitObject;
                selectedSector = sectorInfo.sector;
                selectedSectorObject.GetComponent<MeshRenderer>().material = selectedSectorMaterial;
            }
            else
            {
                if(selectedSector != null)
                {
                    selectedSectorObject.GetComponent<MeshRenderer>().material = selectedSector.sectorModelPrefab.GetComponent<MeshRenderer>().sharedMaterial;
                }
                selectedSectorObject = null;
                selectedSector = null;
            }
        }
        
    }
}
