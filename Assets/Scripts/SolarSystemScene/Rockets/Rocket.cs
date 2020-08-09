using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RocketType
{
    Colonial = 0,
    Trade = 1,
    Cargo = 2,
    Tourist = 3
}

public enum RocketStatus
{
    Idle = 0,
    Connection = 1
}

public class RocketQueueItem
{
    public RocketType rocketType;
    public int constructionTime;
}

public class Rocket
{
    public RocketType rocketType;
    public RocketStatus status;
    public Planet planetA;
    public Planet planetB;
    public bool hasConnection;
    public int carryValue;
}