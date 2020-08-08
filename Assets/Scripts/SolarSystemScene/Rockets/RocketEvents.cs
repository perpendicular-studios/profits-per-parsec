using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public float rocketLaunchTimeDelay = 2;
    public GameObject rocketPrefab;

    private float actionTime;
    private float elapsedTime;
    private bool launched = false;
    private IEnumerator coroutine;

    void Awake()
    {
        actionTime = rocketLaunchTimeDelay;
        coroutine = Launch();
        StartCoroutine(coroutine);
    }
    /*
    void FixedUpdate()
    {
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime > actionTime)
        {
            int currentTime = (int)Time.unscaledDeltaTime;
            foreach (RocketPath rocketMovement in RocketController.instance.queuedRockets)
            {

                // Check if rocket path is queued
                string startPosition = rocketMovement.startPosition;
                string target = rocketMovement.target;

                GameObject rocket = Instantiate(rocketPrefab);
                rocket.GetComponent<RocketMovement>().startPositionString = startPosition;
                rocket.GetComponent<RocketMovement>().targetString = target;

                RocketController.instance.activeRockets.Add(rocket);

                
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
    */
    private IEnumerator Launch()
    {
        while (true)
        {
            foreach (RocketPath rocketMovement in RocketController.instance.queuedRockets)
            {

                // Check if rocket path is queued
                string startPosition = rocketMovement.startPosition;
                string target = rocketMovement.target;

                GameObject rocket = Instantiate(rocketPrefab);
                rocket.GetComponent<RocketMovement>().startPositionString = startPosition;
                rocket.GetComponent<RocketMovement>().targetString = target;

                RocketController.instance.activeRockets.Add(rocket);
                yield return new WaitForSeconds(2);
            }
            RocketController.instance.queuedRockets.Clear();
            yield return new WaitForSeconds(2);

        }
    }
}

