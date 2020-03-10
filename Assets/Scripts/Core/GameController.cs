using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool _isShuttingDown;
    private static object _lock = new object();
    private static T _instance;

    public static T instance {
        get
        {
            if(_isShuttingDown)
            {
                Debug.Log($"[Controller] instance of type {typeof(T)} already destroyed, returning null.");
                return null;
            }

            lock(_lock)
            {
                if(_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if(_instance == null)
                    {
                        GameObject controller = new GameObject();
                        _instance = controller.AddComponent<T>();
                        controller.name = $"{typeof(T)} Singleton";

                        DontDestroyOnLoad(controller);
                    }
                }
                return _instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _isShuttingDown = true;
    }

    private void OnDestroy()
    {
        _isShuttingDown = true;
    }


}
