using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketEvents : MonoBehaviour
{
    public GameObject rocket;
    public delegate void LaunchRocket(Transform startPosition, Transform target);
    public static event LaunchRocket OnRocketLaunch;

    [SerializeField] public Transform target;
    [SerializeField] public Transform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        OnRocketLaunch += Launch;
        RocketMovement.OnRocketLand += Land;
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke Launch Rocket event 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnRocketLaunch.Invoke(startPosition, target);
        }
    }

    void Launch(Transform startPosition, Transform target)
    {
        var newRocket = Instantiate(rocket);
        newRocket.GetComponent<RocketMovement>().startPosition = startPosition;
        newRocket.GetComponent<RocketMovement>().target = target;
    }

    void Land(Transform target)
    {
        Debug.Log("Destroyed");
    }
}
