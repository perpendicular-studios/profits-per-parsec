using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public float rocketLaunchTimeDelay = 10;
    public GameObject rocketPrefab;

    private float actionTime;
    private float elapsedTime;

    // TODO : Move these to rocket UI screen
    public delegate void LaunchRocket(string startPosition, string target);
    public static event LaunchRocket OnRocketLaunch;

    public delegate void DestroyRocket(string startPosition, string target);
    public static event DestroyRocket OnRocketDestroy;

    void Awake()
    {
        RocketController.instance.rocketPrefab = rocketPrefab;
        actionTime = rocketLaunchTimeDelay;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnRocketLaunch?.Invoke("Venus", "Uranus");
        }

        if(elapsedTime > actionTime)
        {
            foreach(GameObject rocketMovementObject in RocketController.instance.queuedRockets) {
                // Check if rocket path is queued
                string startPosition = rocketMovementObject.GetComponent<RocketMovement>().startPositionString;
                string target = rocketMovementObject.GetComponent<RocketMovement>().targetString;

                if (RocketController.instance.IsRocketPathQueued(startPosition, target))
                {
                    rocketMovementObject.SetActive(true);
                    RocketController.instance.activeRockets.Add(rocketMovementObject);
                }
            }


            elapsedTime = 0;
        }

        elapsedTime += Time.deltaTime;
    }
}

