using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : GameController<PlayerStatController>
{
    private int _cash;

    [Range(-1f, 1f)]
    private float _growthRate;

    private int _publicRelation;

    [Range(-100, 100)]
    private int _governmentSupport;

    public int cash { get { return _cash; } set { _cash = value; } }
    public float growthRate { get { return _growthRate; } set { _growthRate = value; } }
    public int governmentSupport { get { return _governmentSupport; } set { _governmentSupport = value; } }
    public int publicRelation { get { return _publicRelation; } set { _publicRelation = value; } }

    void Start() {}

    void Update()
    {
        
    }
}
