using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInfo
{

    private float posX;
    private float posY;
    private float posZ;
    private float rotX;
    private float rotY;
    private float rotZ;
    private float camPosX;
    private float camPosY;
    private float camPosZ;
    private float zoomLevel;

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

    public Vector3 GetPositionVector()
    {
        return new Vector3(posX, posY, posZ);
    }

    public Vector3 GetRotationVector()
    {
        return new Vector3(rotX, rotY, rotZ);
    }

    public Vector3 GetCameraPositionVector()
    {
        return new Vector3(camPosX, camPosY, camPosZ);
    }

    public float GetZoomLevel()
    {
        return zoomLevel;
    }

    public override string ToString()
    {
        return $"{posX}, {posY}, {posZ}, {rotX}, {rotY}, {rotZ}, {camPosX}, {camPosY}, {camPosZ}, {zoomLevel}";
    }
}
