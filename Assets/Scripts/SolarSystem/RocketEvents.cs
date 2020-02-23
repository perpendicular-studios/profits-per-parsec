using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public GameObject rocket;
    public float rocketLaunchTimeDelay = 10;

    private static int numRockets = 0;
    private static string target;
    private static string startPosition;
    private float actionTime;

    void Awake()
    {
        actionTime = rocketLaunchTimeDelay;
    }

    void OnEnable()
    {
        RocketController.OnRocketLaunch += Launch;
        RocketMovement.OnRocketLand += Land;
    }


    // Update is called once per frame
    void Update()
    {
        actionTime += Time.deltaTime;
        if (numRockets > 0 && actionTime > rocketLaunchTimeDelay)
        {
            var newRocket = Instantiate(rocket);
            newRocket.GetComponent<RocketMovement>().startPosition = GameObject.Find(startPosition).transform;
            newRocket.GetComponent<RocketMovement>().target = GameObject.Find(target).transform;

            actionTime = 0;
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
