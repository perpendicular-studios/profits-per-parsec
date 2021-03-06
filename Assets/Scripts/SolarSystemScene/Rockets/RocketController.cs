﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RocketPath
{
    public Transform startPosition;
    public Transform target;

    public RocketPath(Transform startPosition, Transform target)
    {
        this.startPosition = startPosition;
        this.target = target;
    }
}

public class RocketController : GameController<RocketController>
{
    public List<RocketPath> queuedRockets = new List<RocketPath>();
    public List<GameObject> activeRockets = new List<GameObject>();
    public void Awake()
    {
        DateTimeController.OnDailyTick += AddIncome;
    }

    public void CreateConnection(Transform startPosition, Transform target)
    {
        queuedRockets.Add(new RocketPath(startPosition, target));
    }

    public void AddIncome()
    {
        // Add income from each active rocket
        PlayerStatController.instance.cash += 5 * activeRockets.Count;
    }
}
