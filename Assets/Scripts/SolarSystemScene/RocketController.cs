using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RocketPath
{
    public string startPosition;
    public string target;

    public RocketPath(string startPosition, string target)
    {
        this.startPosition = startPosition;
        this.target = target;
    }
}

public class RocketController : GameController<RocketController>
{
    public List<RocketPath> queuedRockets = new List<RocketPath>();
    public List<GameObject> activeRockets = new List<GameObject>();

    public void OnEnable()
    {
        SectorManager.OnRocketLaunch += CreateRocket;
        SectorManager.OnRocketDestroy += RemoveRocket;
    }

    public void OnDisable()
    {
        SectorManager.OnRocketLaunch -= CreateRocket;
        SectorManager.OnRocketDestroy -= RemoveRocket;
    }

    public void CreateRocket(string startPosition, string target)
    {
        if (!IsRocketPathQueued(startPosition, target))
        {
            Debug.Log($"A rocket is being created from {startPosition} to {target} ");
            queuedRockets.Add(new RocketPath(startPosition, target));
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

        foreach(RocketPath rocketMovement in queuedRockets)
        {
            string queuedStartPosition = rocketMovement.startPosition;
            string queuedTarget = rocketMovement.target;

            if (queuedStartPosition.Equals(startPosition)) startPositionQueued = true;
            if (queuedTarget.Equals(target)) targetQueued = true;
        }

        return startPositionQueued && targetQueued;
    }
    public bool IsRocketPathActive(string startPosition, string target)
    {
        bool startPositionActive = false;
        bool targetActive = false;

        foreach (GameObject rocketMovementObj in activeRockets)
        {
            string activeStartPosition = rocketMovementObj.GetComponent<RocketMovement>().startPositionString;
            string activeTarget = rocketMovementObj.GetComponent<RocketMovement>().targetString;

            if (activeStartPosition.Equals(startPosition)) startPositionActive = true;
            if (activeTarget.Equals(target)) targetActive = true;
        }

        return startPositionActive && targetActive;
    }
}
