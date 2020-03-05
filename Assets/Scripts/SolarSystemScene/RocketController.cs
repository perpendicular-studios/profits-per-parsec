using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    public delegate void LaunchRocket(RocketController controller);
    public static event LaunchRocket OnRocketLaunch;

    public delegate void DestroyRocket(RocketController controller);
    public static event DestroyRocket OnRocketDestroy;

    public string target;
    public string startPosition;

    void OnEnable()
    {
        OnRocketLaunch?.Invoke(this);
    }

    public void RemoveRocket()
    {
        OnRocketDestroy?.Invoke(this);
    }
}
