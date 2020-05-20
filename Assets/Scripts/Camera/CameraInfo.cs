using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInfo
{

    public float posX;
    public float posY;
    public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;
    public float camPosX;
    public float camPosY;
    public float camPosZ;
    public float zoomLevel;

    public CameraInfo()
    {
        posX = posY = posZ = rotX = rotY = rotZ = camPosX = camPosY = camPosZ = zoomLevel = 0;
    }

    public CameraInfo(float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float camPosX, float camPosY, float camPosZ, float zoomLevel)
    {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
        this.camPosX = camPosX;
        this.camPosY = camPosY;
        this.camPosZ = camPosZ;
        this.zoomLevel = zoomLevel;
    }

    public override string ToString()
    {
        return $"{posX}, {posY}, {posZ}, {rotX}, {rotY}, {rotZ}, {camPosX}, {camPosY}, {camPosZ}, {zoomLevel}";
    }
}
