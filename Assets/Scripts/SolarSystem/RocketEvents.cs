using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public GameObject rocket;
    public static int numRockets = 0;
    private static string target;
    private static string startPosition;

    // Start is called before the first frame update
    void Start()
    {
        RocketController.OnRocketLaunch += Launch;
        RocketMovement.OnRocketLand += Land;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numRockets);
        //Invoke Launch Rocket event 
        if (numRockets>0)
        {
            var newRocket = Instantiate(rocket);
            newRocket.GetComponent<RocketMovement>().startPosition = GameObject.Find(startPosition).transform;
            newRocket.GetComponent<RocketMovement>().target = GameObject.Find(target).transform;
            numRockets--;
        }
    }

    void Launch(string startPosition, string target)
    {
        RocketEvents.startPosition = startPosition;
        RocketEvents.target = target;
        Debug.Log("Launch");
        numRockets++;  
    }

    void Land(Transform target)
    {
        Debug.Log("Destroyed");
    }
}
