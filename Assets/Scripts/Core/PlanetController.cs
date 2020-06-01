using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

public class PlanetController : GameController<PlanetController>
{
    public Planet sun;
    public List<Planet> planets;

    public int timePassed = 0;

    private void OnEnable()
    {
        DateTimeController.OnDailyTick += IncrementTime;
    }

    private void OnDisable()
    {
        DateTimeController.OnDailyTick -= IncrementTime;
    }

    private void Start()
    {
        
    }

    void IncrementTime()
    {
        timePassed++;
    }

    //Used to reset time counter to 0 when entering a planet to update planet locations upon reloading the solar system scene
    public void resetTimeCounter()
    {
        timePassed = 0;
    }
}
