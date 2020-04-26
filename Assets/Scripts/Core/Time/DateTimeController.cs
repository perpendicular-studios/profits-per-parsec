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
    public int speed = 1;

    private double realTime = 0;

    public delegate void DailyTick();
    public static event DailyTick OnDailyTick;

    public delegate void MonthlyTick();
    public static event MonthlyTick OnMonthlyTick;

    public delegate void AnnualTick();
    public static event AnnualTick OnAnnualTick;

    public void FixedUpdate()
    {
        if (realTime > 1)
        {
            currentDay++;
            realTime = 0;
            OnDailyTick?.Invoke();
        }

        if (currentDay > 31)
        {
            currentMonth++;
            currentDay = 0;
            OnMonthlyTick?.Invoke();
        }

        if (currentMonth > 12)
        {
            currentYear++;
            currentMonth = 0;
            OnAnnualTick?.Invoke();
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

    public void SlowDown()
    {
        if (speed > 1)
        {
            speed--;
            Time.timeScale = speed;
        }
    }

    public void SpeedUp()
    {
        if (speed < DateTimeConstants.MAX_TIME_SPEED)
        {
            speed++;
            Time.timeScale = speed;
        }
    }

    public string GetFormattedDateTime()
    {
        return $"{currentYear}/{currentMonth}/{currentDay}";
    }
}
