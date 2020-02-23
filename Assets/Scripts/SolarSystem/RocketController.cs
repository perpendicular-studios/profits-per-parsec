using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    public delegate void LaunchRocket(string startPosition, string target);
    public static event LaunchRocket OnRocketLaunch;
    [SerializeField] string target;
    [SerializeField] string startPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnRocketLaunch?.Invoke(startPosition, target);
        }
    }
}
