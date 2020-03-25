using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputController : InputController
{

    private Vector2Int screen;
    private float mousePositionOnRotateStart;

    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotate;
    public static event ZoomInputHandler OnZoom;

    void Start()
    {
        screen = new Vector2Int(Screen.width, Screen.height);   
    }

    void Update()
    {
        Vector3 mp = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            mousePositionOnRotateStart = mp.x;
        }
        else if (Input.GetMouseButton(1))
        {
            if (mp.x < mousePositionOnRotateStart)
            {
                OnRotate?.Invoke(-1);
            }
            else if (mp.x > mousePositionOnRotateStart)
            {
                OnRotate?.Invoke(1);
            }
        }

        if (Input.mouseScrollDelta.y > 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            OnZoom?.Invoke(-3f);
        }
        else if (Input.mouseScrollDelta.y < 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            OnZoom?.Invoke(3f);
        }
    }
}
