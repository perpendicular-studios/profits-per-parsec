using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelfRotate : MonoBehaviour
{
    // Initializing Variables
    public float rotationSpeed;      
    public float dampAmt;            // Adjust rotation speed more finely

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Vector3.up * rotationSpeed) * (Time.deltaTime * dampAmt), Space.Self);
    }
}
