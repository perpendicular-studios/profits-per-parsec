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

            GameObject rocket = Instantiate(rocketPrefab);
            rocket.GetComponent<RocketMovement>().startPosition = rocketMovement.startPosition;
            rocket.GetComponent<RocketMovement>().target = rocketMovement.target;

            RocketController.instance.activeRockets.Add(rocket);
            yield return new WaitForSeconds(RocketConstants.ROCKET_QUEUE_DELAY);
        }
        RocketController.instance.queuedRockets.Clear();
        yield return new WaitForSeconds(RocketConstants.ROCKET_QUEUE_DELAY);
        launchCoroutine = null;
    }
}

