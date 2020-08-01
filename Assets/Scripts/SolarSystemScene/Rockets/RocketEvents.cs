using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public float rocketLaunchTimeDelay = 15;
    public GameObject rocketPrefab;

    private float actionTime;
    private float elapsedTime;
    private bool launched = false;

    void Awake()
    {
        actionTime = rocketLaunchTimeDelay;
    }

    void FixedUpdate()
    {
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime > actionTime)
        {
            
            foreach (RocketPath rocketMovement in RocketController.instance.queuedRockets)
            {
                // Check if rocket path is queued
                string startPosition = rocketMovement.startPosition;
                string target = rocketMovement.target;

                if (RocketController.instance.IsRocketPathQueued(startPosition, target))
                {
                    GameObject rocket = Instantiate(rocketPrefab);
                    rocket.GetComponent<RocketMovement>().startPositionString = startPosition;
                    rocket.GetComponent<RocketMovement>().targetString = target;

                    RocketController.instance.activeRockets.Add(rocket);
                }
            }

            // Clear all queued rockets once they have been launched.
            RocketController.instance.queuedRockets.Clear();

            elapsedTime = 0;
        }
    }
}

