using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelfRotate : MonoBehaviour
{
    
    // Initializing Variables
    public float rotationSpeed;      
    public float dampAmt;            // Adjust rotation speed more finely
    public Transform rotatingObject;

    // Update is called once per frame
    void Update()
    {
        rotatingObject.Rotate((Vector3.up * rotationSpeed/2) * (Time.deltaTime * dampAmt), Space.Self);
    }
}
