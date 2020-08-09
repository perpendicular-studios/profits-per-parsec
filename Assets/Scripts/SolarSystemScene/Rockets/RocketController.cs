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

    }

    public void OnDisable()
    {

    }

    public void CreateConnection(string startPosition, string target)
    {
        queuedRockets.Add(new RocketPath(startPosition, target));
    }

}
