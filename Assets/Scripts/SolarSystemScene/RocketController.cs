using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : GameController<RocketController>
{
    public List<GameObject> queuedRockets = new List<GameObject>();
    public List<GameObject> activeRockets = new List<GameObject>();
    public GameObject rocketPrefab;

    public void OnEnable()
    {
        RocketEvents.OnRocketLaunch += CreateRocket;
        RocketEvents.OnRocketDestroy += RemoveRocket;
    }

    public void OnDisable()
    {
        RocketEvents.OnRocketLaunch -= CreateRocket;
        RocketEvents.OnRocketDestroy -= RemoveRocket;
    }

    public void CreateRocket(string startPosition, string target)
    {
        if (!IsRocketPathQueued(startPosition, target))
        {
            Debug.Log($"A rocket is being created from {startPosition} to {target} ");

            GameObject rocketObject = Instantiate(rocketPrefab);
            rocketObject.GetComponent<RocketMovement>().startPositionString = startPosition;
            rocketObject.GetComponent<RocketMovement>().targetString = target;
            rocketObject.SetActive(false);

            queuedRockets.Add(rocketObject);
        }
        else
        {
            Debug.Log($"A rocket from {startPosition} to {target} already exists!");
        }
    }

    public void RemoveRocket(string startPosition, string target)
    {
        Debug.Log("A rocket is being destroyed");
    }

    public bool IsRocketPathQueued(string startPosition, string target)
    {
        bool startPositionQueued = false;
        bool targetQueued = false;

        foreach(GameObject rocket in queuedRockets)
        {
            string queuedStartPosition = rocket.GetComponent<RocketMovement>().startPositionString;
            string queuedTarget = rocket.GetComponent<RocketMovement>().targetString;

            if (queuedStartPosition.Equals(startPosition)) startPositionQueued = true;
            if (queuedTarget.Equals(target)) targetQueued = true;
        }

        return startPositionQueued && targetQueued;
    }
}
