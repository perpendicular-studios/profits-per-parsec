using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private ZoomStrategy zoomStrategy;
    private Vector3 frameMove;
    private float frameRotate;
    private float frameZoom;

    private Camera cam;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
        zoomStrategy = new PerspectiveZoomStrategy(cam, cameraOffset, initialZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
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
