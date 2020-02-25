using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public GameObject rocket;
    public float rocketLaunchTimeDelay = 10;

    private float actionTime;
    private static List<RocketController> queuedRockets = new List<RocketController>();

    void Awake()
    {
        actionTime = rocketLaunchTimeDelay;
    }

    void OnEnable()
    {
        RocketController.OnRocketLaunch += Launch;
        RocketController.OnRocketDestroy += DestroyRocket;
        RocketMovement.OnRocketLand += Land;
    }


    // Update is called once per frame
    void Update()
    {
        actionTime += Time.deltaTime;
        if (queuedRockets.Count > 0 && actionTime > rocketLaunchTimeDelay)
        {
            foreach(RocketController queuedRocket in queuedRockets) {
                var newRocket = Instantiate(rocket);
                newRocket.GetComponent<RocketMovement>().startPosition = GameObject.FindGameObjectWithTag(queuedRocket.startPosition).GetComponent<OrbitMotion>().orbitingObject.GetComponent<Transform>();
                newRocket.GetComponent<RocketMovement>().target = GameObject.FindGameObjectWithTag(queuedRocket.target).GetComponentInChildren<OrbitMotion>().orbitingObject.GetComponent<Transform>();
            }
            actionTime = 0;
        }
    }

    void Launch(RocketController controller)
    {
        queuedRockets.Add(controller);
        Debug.Log($"Launch from {controller.startPosition} to {controller.target}");
        Debug.Log($"There are currently {queuedRockets.Count} rockets going from {controller.startPosition} to {controller.target}");
    }

    void Land(Transform target)
    {
        Debug.Log("Destroyed");
    }

    void DestroyRocket(RocketController controller)
    {
        queuedRockets.Remove(controller);
        Debug.Log($"Deleted Launch from {controller.startPosition} to {controller.target}");
        Debug.Log($"There are currently {queuedRockets.Count} rockets going from {controller.startPosition} to {controller.target}");
    }
}
