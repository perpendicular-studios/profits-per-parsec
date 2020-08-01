using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionDisplay : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildRocket()
    {
        Planet currPlanet = PlanetController.instance.currentPlanet;
        //Check if the planet has enough space for a new rocket
        if (currPlanet.idleRockets + currPlanet.currConnections < currPlanet.maxConnections)
        {
            //Queue
            //Build a rocket
            currPlanet.idleRockets++;
            Debug.Log("Rocket Built!");
        }
        //Rocket Capacity Full
        else
        {
            Debug.Log("Rocket Capacity Full!");
        }
    }
}
