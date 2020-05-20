using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [Header("Camera Position Settings")]
    public Vector2 cameraOffset = new Vector2(10f, 14f);
    public float lookAtOffset = 2f;
    public float initialZoom = 5f;

    [Header("Camera Movement Settings")]
    public float inOutSpeed = 5f;
    public float lateralSpeed = 5f;
    public float rotateSpeed = 45f;
    public float zoomSpeed = 4f;

    [Header("Camera Bounds")]
    public Vector2 minBounds = new Vector2(-100, -100);
    public Vector2 maxBounds = new Vector2(100, 100);
    public float nearZoomLimit = 2f;
    public float farZoomLimit = 20f;

    public ZoomStrategy zoomStrategy;
    private Vector3 frameMove;
    private float frameRotate;
    public float frameZoom;

    private Camera cam;
    

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        

        //Define dictionary in player controller
        if (PlayerStatController.instance.cameraList == null)
        {
            PlayerStatController.instance.cameraList = new Dictionary<string, CameraInfo>();
        }

        //If current scene is loaded for first time 
        if (!PlayerStatController.instance.cameraList.ContainsKey(SceneManager.GetActiveScene().name))
        {
            //Set intial coords
            cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));

        }

        zoomStrategy = new PerspectiveZoomStrategy(cam, cameraOffset, initialZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);

        //If scene was never loaded create dictionary key value
        if (!PlayerStatController.instance.cameraList.ContainsKey(SceneManager.GetActiveScene().name))
        {
            PlayerStatController.instance.cameraList.Add(SceneManager.GetActiveScene().name, new CameraInfo());

        }
        // If scene was loaded set camera to dictionary value
        else
        {
            PerspectiveZoomStrategy zs = (zoomStrategy) as PerspectiveZoomStrategy;
            transform.position = new Vector3(
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].posX,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].posY,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].posZ
                );
            transform.eulerAngles = new Vector3(
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].rotX,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].rotY,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].rotZ
                );
            cam.transform.localPosition = new Vector3(
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].camPosX,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].camPosY,
                PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].camPosZ
                );
            zs.currentZoomLevel = PlayerStatController.instance.cameraList[SceneManager.GetActiveScene().name].zoomLevel;
        }
    }

    void LateUpdate()
    {
        if(frameMove != Vector3.zero)
        {
            float xMoveWithSpeed = frameMove.x * lateralSpeed;
            float zMoveWithSpeed = frameMove.z * inOutSpeed;

            /* Slow down when player is pressing shift while moving */
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                xMoveWithSpeed /= 2;
                zMoveWithSpeed /= 2;
            }

            Vector3 frameMoveWithSpeed = new Vector3(xMoveWithSpeed, frameMove.y, zMoveWithSpeed);
            transform.position += transform.TransformDirection(frameMoveWithSpeed) * Time.unscaledDeltaTime;
            CalculateBounds();
            frameMove = Vector3.zero;
        }

        if(frameRotate != 0)
        {
            transform.Rotate(Vector3.up, frameRotate * Time.unscaledDeltaTime * rotateSpeed);
            frameRotate = 0;
        }

        if(frameZoom < 0f)
        {
            zoomStrategy.ZoomIn(cam, Time.unscaledDeltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0f;
        }
        else if(frameZoom > 0f)
        {
            zoomStrategy.ZoomOut(cam, Time.unscaledDeltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0f;
        }
    }

    private void CalculateBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y));
    }

    void OnEnable()
    {
        MouseInputController.OnMoveInput += UpdateFrameMove;
        MouseInputController.OnRotate += UpdateFrameRotate;
        MouseInputController.OnZoom += UpdateFrameZoom;
        KeyboardInputController.OnMoveInput += UpdateFrameMove;
        KeyboardInputController.OnRotate += UpdateFrameRotate;
        KeyboardInputController.OnZoom += UpdateFrameZoom;
    }

    void OnDisable()
    {
        MouseInputController.OnMoveInput -= UpdateFrameMove;
        MouseInputController.OnRotate -= UpdateFrameRotate;
        MouseInputController.OnZoom -= UpdateFrameZoom;
        KeyboardInputController.OnMoveInput -= UpdateFrameMove;
        KeyboardInputController.OnRotate -= UpdateFrameRotate;
        KeyboardInputController.OnZoom -= UpdateFrameZoom;
    }

    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }

    private void UpdateFrameRotate(float degrees)
    {
        frameRotate += degrees;
    }

    private void UpdateFrameZoom(float zoomFactor)
    {
        frameZoom += zoomFactor;
    }

}
