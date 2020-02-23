using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public delegate void MoveInputHandler(Vector3 moveVector);
    public delegate void RotateInputHandler(float degrees);
    public delegate void ZoomInputHandler(float zoomFactor);

}
