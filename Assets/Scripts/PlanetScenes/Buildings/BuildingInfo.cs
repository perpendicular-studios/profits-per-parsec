using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    private Building _building;
    public Building building
    {
        get { return _building; }
        set { _building = value; }
    }

    private float _posX;
    private float _posY;
    private float _posZ;

    public BuildingInfo(Building building, float posX, float posY, float posZ)
    {
        _building = building;
        _posX = posX;
        _posY = posY;
        _posZ = posZ;
    }

    public Vector3 GetPositionVector()
    {
        return new Vector3(_posX, _posY, _posZ);
    }

}
