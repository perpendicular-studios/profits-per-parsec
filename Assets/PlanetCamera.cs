using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    private Transform target;
    public float speed = 5;
    public float minFov = 35f;
    public float maxFov = 100f;
    public float sensitivity = 17f;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButton(2) && target)
        {
            transform.RotateAround(target.position, transform.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(target.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
        }

        //Zoom
        float fov = GetComponent<Camera>().fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        GetComponent<Camera>().fieldOfView = fov;

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget.GetComponent<Tile>().parentPlanet.gameObject.transform;
        transform.position = newTarget.position + newTarget.up * target.GetComponent<Hexsphere>().planet.worldScale;
        transform.LookAt(target);
        transform.parent = target;
    }
}
