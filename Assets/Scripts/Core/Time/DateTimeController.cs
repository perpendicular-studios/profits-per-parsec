using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DateTimeController : GameController<DateTimeController> {

    private int currentDay = 0;
    private int currentMonth = 0;
    private int currentYear = 0;

    public bool paused = false;

    private double realTime = 0;

    public void FixedUpdate()
    {
        if (realTime > 1)
        {
            currentDay++;
            realTime = 0;
        }

        if (currentDay > 31)
        {
            currentMonth++;
            currentDay = 0;
        }

        if (currentMonth > 12)
        {
            currentYear++;
            currentMonth = 0;
        }
        
        realTime += Time.deltaTime;   
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void Play()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public string GetFormattedDateTime()
    {
        return $"{currentYear}/{currentMonth}/{currentDay}";
    }
}
