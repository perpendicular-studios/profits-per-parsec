using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public GameObject rocketPrefab;
    private IEnumerator launchCoroutine;

    void Awake()
    {

    }
   
    void FixedUpdate()
    {
        if(launchCoroutine == null)
        {
            launchCoroutine = Launch();
            StartCoroutine(launchCoroutine);
        }

    }

    private IEnumerator Launch()
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
            yield return new WaitForSeconds(RocketConstants.ROCKET_QUEUE_DELAY);
        }
        RocketController.instance.queuedRockets.Clear();
        yield return new WaitForSeconds(RocketConstants.ROCKET_QUEUE_DELAY);
        launchCoroutine = null;
    }
}

